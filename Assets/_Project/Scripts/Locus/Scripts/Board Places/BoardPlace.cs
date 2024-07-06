using System;
using UnityEngine;

[RequireComponent(typeof(BoardPlaceVisuals))]
public class BoardPlace : MonoBehaviour {
    public static Action OnBoardPlaceSelected;

    [field:SerializeField] public bool IsPlayerPlace { get; private set; }
    [field:SerializeField] public bool IsMonsterPlace { get; private set; }
    [field:SerializeField] public bool IsFree { get; private set; }
    private bool _canBeSelected;
    private Card _resultCard;
    public Card _cardInPlace;

    private void OnEnable() {
        BoardPlaceSelectionPhase.OnBoardPlaceSelectionStart += BoardPlaceSelectionPhase_OnBoardPlaceSelectionStart;
        OnBoardPlaceSelected += This_OnBoardPlaceSelected;
    }
    
    private void OnDisable() {
        BoardPlaceSelectionPhase.OnBoardPlaceSelectionStart -= BoardPlaceSelectionPhase_OnBoardPlaceSelectionStart;
        OnBoardPlaceSelected -= This_OnBoardPlaceSelected;
    }

    private void This_OnBoardPlaceSelected(){
        _canBeSelected = false;
    }

    private void Start(){
        IsFree = true;
    }

    private void BoardPlaceSelectionPhase_OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn){
        if(IsPlayerPlace && isPlayerTurn){
            _resultCard = card;
            if(IsMonsterPlace && _resultCard is MonsterCard){
                _canBeSelected = true;
            }else if(!IsMonsterPlace && _resultCard is ArcaneCard){
                _canBeSelected = true;
            }
        }
    }

    private void OnMouseDown() {
        if(!_canBeSelected) { return; }
        if(IsFree){
            SetCardInPlace(_resultCard);
        }else{
            //Fusion Logic with the monster in This Place
        }
    }

    public void SetCardInPlace(Card card){
        if(card is MonsterCard){
            var monsterCard = card as MonsterCard;
            if(monsterCard.IsInAttackMode){//In attacK
                if(monsterCard.IsFaceDown){// In Attack Face Down
                Debug.Log("Attack Face Down");
                    Quaternion rotation = Quaternion.Euler(-90, -90, -90);
                    card.MoveCard(transform, rotation);
                }else{ // In Attack Face Up
                    Debug.Log("Attack Face Up");
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
        }else{// Arcane Card

        }

        _cardInPlace = card;
        _canBeSelected = false;
        IsFree = false;
        OnBoardPlaceSelected?.Invoke();
    }
}