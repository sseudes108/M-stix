using System;
using UnityEngine;

[RequireComponent(typeof(BoardPlaceVisuals))]
public class BoardPlace : MonoBehaviour {
    public static Action OnBoardPlaceSelected;

    [field:SerializeField] public bool IsPlayerPlace { get; private set; }
    [field:SerializeField] public bool IsMonsterPlace { get; private set; }
    [field:SerializeField] public bool IsFree { get; private set; }
    public bool _canBeSelected;
    public Card _resultCard;

    private void OnEnable() {
        BoardPlaceSelectionPhase.OnBoardPlaceSelectionStart += BoardPlaceSelectionPhase_OnBoardPlaceSelectionStart;
    }
    
    private void OnDisable() {
        BoardPlaceSelectionPhase.OnBoardPlaceSelectionStart -= BoardPlaceSelectionPhase_OnBoardPlaceSelectionStart;
    }

    private void BoardPlaceSelectionPhase_OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn){
        if(this.IsPlayerPlace && isPlayerTurn){
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
        SetCardInPlace(_resultCard);
    }

    public void SetCardInPlace(Card card){
        card.MoveCard(transform);
        OnBoardPlaceSelected?.Invoke();
    }
}