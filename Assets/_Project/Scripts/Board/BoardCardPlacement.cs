using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BoardCardPlacement : MonoBehaviour {
    [SerializeField] protected bool _isFree;
    protected Collider _collider;
    protected Renderer _renderer;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private Button _flipCard;

    [SerializeField] private Card _cardInThisPlace;

    protected void Awake() {
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void Start() {
        SetPlaceFree();
    }
    
    private void OnMouseOver() {
        //Change ilustration in the ui hold card when the card itself has the collider off;
        var currentPhase = BattleManager.Instance.BattleStateManager.CurrentPhase;
        if(currentPhase == BattleManager.Instance.BoardPlaceSelectionPhase || currentPhase == BattleManager.Instance.ActionPhase){
            if(!_isFree){
                BattleManager.Instance.UIBattleManager.UICardPlaceHolder.ChangeIllustration(_cardInThisPlace.Ilustration);
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
        }else if(currentPhase == BattleManager.Instance.ActionPhase){
            if(!IsFree()){
                _canvas.SetActive(true);
                if(_cardInThisPlace.IsFaceDown()){
                    _flipCard.gameObject.SetActive(true);
                }else{
                    _flipCard.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnMouseExit() {
        if(_canvas != null){
            if(_canvas.activeSelf){
                _canvas.SetActive(false);
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
            SetPlaceOcuppied();
            SetCardInThisPlace(resultCard);
            BattleManager.Instance.BoardPlaceManager.SetLastCardPlaced(resultCard);

            yield return new WaitForSeconds(1f);
            BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.ActionPhase);
        }
    }

    public void SetPlaceFree(){
        _cardInThisPlace = null; 
        _isFree = true;
    }

    public void SetPlaceOcuppied(){_isFree = false;}
    public bool IsFree() => _isFree;

    public virtual Renderer Renderer => _renderer;
    protected virtual void SetMonsterCardRotation(CardMonster resultCard){}
    protected virtual void SetArcaneCardRotation(CardArcane resultCard){}

    private void SetCardInThisPlace(Card card){
        // _cardInThisPlace = null;
        _cardInThisPlace = card;
    }

    public void DisableCardColliderInBoardPhaseSelection(){
        // var card = GetComponentInChildren<Card>();
        Debug.Log("DisableCardColliderInBoardPhaseSelection" + _cardInThisPlace.name); 
        // if(card != null){
        //     card.DisableCollider();
        // }
        _cardInThisPlace.DisableCollider();
    }
    public void EnableCardColliderInBoardPhaseSelection(){
        // var card = GetComponentInChildren<Card>();
        Debug.Log("EnableCardColliderInBoardPhaseSelection" + _cardInThisPlace.name); 
        // if(card != null){
        //     card.EnableCollider();
        // }
        _cardInThisPlace.EnableCollider();
    }
}