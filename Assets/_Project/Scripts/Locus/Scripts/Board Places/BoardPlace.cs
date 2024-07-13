
using System;
using UnityEngine;

[RequireComponent(typeof(BoardPlaceVisuals))]
public class BoardPlace : MonoBehaviour {
    [SerializeField] private BattleManagerSO BattleManager;
    [SerializeField] private BoardPlaceEventHandlerSO BoardManager;
    [SerializeField] private UIEventHandlerSO UIManager;

    [field:SerializeField] public EBoardPlace Location { get; private set; }
    [field:SerializeField] public Collider[] Colliders { get; private set; }
    [field:SerializeField] public bool IsPlayerPlace { get; private set; }
    [field:SerializeField] public bool IsMonsterPlace { get; private set; }
    [field:SerializeField] public bool IsFree { get; private set; }
    private bool _canBeSelected;
    private Card _resultCard;
    public Card Card;
    private bool _isOptShowing;

    private void OnEnable() {
        BattleManager.OnBoardPlaceSelectionStart.AddListener(BattleManager_OnBoardPlaceSelectionStart);
        BoardManager.OnBoardPlaceSelected.AddListener(BoardManager_OnBoardPlaceSelected);
        // UIManager.OnMonsterAttack.AddListener(UIManager_OnMonsterAttack);
    }
    
    private void OnDisable() {
        BattleManager.OnBoardPlaceSelectionStart.RemoveListener(BattleManager_OnBoardPlaceSelectionStart);
        BoardManager.OnBoardPlaceSelected.RemoveListener(BoardManager_OnBoardPlaceSelected);
        // UIManager.OnMonsterAttack.RemoveListener(UIManager_OnMonsterAttack);
    }

    private void UIManager_OnMonsterAttack(Card card, bool isPlayerTurn){
        if(isPlayerTurn && !IsPlayerPlace){
            if(!IsFree){
                _canBeSelected = true;
            }
        }
    }

    private void BoardManager_OnBoardPlaceSelected(){
        _canBeSelected = false;
    }

    private void Awake() {
        Colliders = GetComponents<Collider>();
    }

    private void Start(){
        IsFree = true;
    }

    private void BattleManager_OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn){
        if(IsPlayerPlace && isPlayerTurn){
            _resultCard = card;
            if(IsMonsterPlace && _resultCard is MonsterCard){
                _canBeSelected = true;
            }else if(!IsMonsterPlace && _resultCard is ArcaneCard){
                _canBeSelected = true;
            }
        }
    }

    private void OnMouseOver(){
        if(Card == null) { return; }
        if(_isOptShowing) { return; }
        BoardManager.ShowOptions(this);
        _isOptShowing = true;
    }

    private void OnMouseExit(){
        BoardManager.HideOptions();
        _isOptShowing = false;
    }

    private void OnMouseDown() {
        if(!_canBeSelected) { return; }

        switch (BattleManager.CurrentPhase){
            case BoardPlaceSelectionPhase:
                if(IsFree){
                    SetCardInPlace(_resultCard);
                }else{
                    //Fusion Logic with the monster in This Place
                }
            break;

            case ActionPhase:
                Debug.Log("Attacked");
            break;
            
            default:
            break;
        }
    
        // if(IsFree){
        //     SetCardInPlace(_resultCard);
        // }else{
        //     //Fusion Logic with the monster in This Place
        // }
    }


    public void SetCardInPlace(Card card){
        if(card is MonsterCard){
            var monsterCard = card as MonsterCard;
            if(monsterCard.IsInAttackMode){//In attacK
                if(monsterCard.IsFaceDown){// In Attack Face Down
                    Quaternion rotation = Quaternion.Euler(-90, -90, -90);
                    card.MoveCard(transform, rotation);
                }else{ // In Attack Face Up
                    card.MoveCard(transform);
                }
            }else{ // In Deffense
                if(monsterCard.IsFaceDown){// In Deffense Face Down
                    Quaternion rotation = Quaternion.Euler(-90, -180, -90);
                    card.MoveCard(transform, rotation);
                }else{ // In Deffense Face Up
                    Quaternion rotation = Quaternion.Euler(90, 90, 0);
                    card.MoveCard(transform, rotation);
                }
            }
            monsterCard.SetCanChangeMode(true);
            monsterCard.SetCanAttack(true);

        }else{// Arcane Card

        }

        Card = card;
        card.SetCardOnHand(false);
        card.DeselectCard();
        card.DisableCollider();
        
        _canBeSelected = false;
        IsFree = false;
        BoardManager.BoardPlaceSelected();
    }
}