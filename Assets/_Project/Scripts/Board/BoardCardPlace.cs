using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BoardCardPlace : MonoBehaviour {
    public static Action<BoardCardPlace, Card> OnFlipCard;
    public static Action<BoardCardPlace, CardMonster> OnMonsterSetOnBoard;
    
    [SerializeField] protected bool _isFree;
    [SerializeField] protected Card _cardInThisPlace;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Button _flipCard;
    protected Collider _collider;
    protected Renderer _renderer;

    protected void Awake() {
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void Start() {
        SetPlaceFree();
    }
    
    protected virtual void OnMouseOver() {
        //Change ilustration in the ui hold card when the card itself has the collider off;
        var currentPhase = BattleManager.Instance.BattleStateManager.CurrentPhase;

        if(!_isFree){
            if(currentPhase == BattleManager.Instance.BoardPlaceSelectionPhase){
                BattleManager.Instance.UIBattleManager.UICardPlaceHolder.ChangeIllustration(_cardInThisPlace.Ilustration);
            }
            
            //Show buttons
            if(currentPhase == BattleManager.Instance.ActionBattlePhase && _canvas != null){
                BattleManager.Instance.UIBattleManager.UICardPlaceHolder.ChangeIllustration(_cardInThisPlace.Ilustration);

                //Show flip button in the face down cards
                _canvas.SetActive(true);
                if(_cardInThisPlace.IsFaceDown()){
                    _flipCard.gameObject.SetActive(true);

                    _flipCard.onClick.AddListener(TriggerFlipCardEvent);
                }else{
                    //Does not show on face up
                    _flipCard.gameObject.SetActive(false);
                }
            }
        }
    }

    protected virtual void OnMouseExit() {
        if(_canvas != null){
            _flipCard.onClick.RemoveAllListeners();
            if(_canvas.activeSelf){
                _canvas.SetActive(false);
            }
        }
    }
    
    private void OnMouseDown() {
        var resultCard = BattleManager.Instance.FusionManager.GetResultCard();
        var currentPhase = BattleManager.Instance.BattleStateManager.CurrentPhase;

        if(currentPhase == BattleManager.Instance.BoardPlaceSelectionPhase){
            if(_isFree){
                //is monster place and monster card, or is arcane place and arcane card
                if(this is BoardCardMonsterPlace && resultCard is CardMonster /* OR */
                    || this is BoardCardArcanePlace && resultCard is CardArcane){
                    SetCardInPlace(resultCard);
                }
            }else{
                BoardFusion(resultCard);
            }
        }
    }

    public void BoardFusion(Card resultCard){
        List<Card> fusionList = new(){_cardInThisPlace, resultCard};

        SetPlaceFree();
        BattleManager.Instance.FusionManager.SetFusionList(fusionList);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
    }

    public void SetCardInPlace(Card resultCard){
        StartCoroutine(MoveCardRoutine(resultCard));
    }

    private IEnumerator MoveCardRoutine(Card resultCard){
        if(resultCard.IsOnField() == false){;
            resultCard.MoveCard(transform);

            if(resultCard is CardMonster){
                SetMonsterCardRotation(resultCard as CardMonster);
            }else{
                SetArcaneCardRotation(resultCard as CardArcane);
            }

            resultCard.SetCardOnField();
            SetPlaceOcuppied(resultCard);
            BattleManager.Instance.BoardPlaceManager.SetLastCardPlaced(resultCard);

            yield return new WaitForSeconds(1f);
            BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.ActionBattlePhase);
        }
    }

    public void SetPlaceFree(){
        _cardInThisPlace = null; 
        _isFree = true;
    }

    public void SetPlaceOcuppied(Card card){
        _cardInThisPlace = card;
        _isFree = false;

        if(card is CardMonster){
            TriggerMonsterSetOnBoardEvent();
        }
    }

    public Card GetCardInThisPlace(){return _cardInThisPlace;}

    public void DisableCardColliderInBoardPhaseSelection(){
        if(_cardInThisPlace != null){
            _cardInThisPlace.DisableCollider();
        }
    }

    public void EnableCardColliderInBoardPhaseSelection(){
        if(_cardInThisPlace != null){
            _cardInThisPlace.EnableCollider();
        }
    }
    
    protected virtual void SetMonsterCardRotation(CardMonster resultCard){}
    protected virtual void SetArcaneCardRotation(CardArcane resultCard){}
    public bool IsFree() => _isFree;
    public virtual Renderer Renderer => _renderer;

    //Events
    private void TriggerFlipCardEvent(){
        if(_cardInThisPlace != null){
            OnFlipCard?.Invoke(this, _cardInThisPlace);
        }
    }

    private void TriggerMonsterSetOnBoardEvent(){
        if(_cardInThisPlace != null){
            OnMonsterSetOnBoard?.Invoke(this, _cardInThisPlace as CardMonster);
        }
    }
}