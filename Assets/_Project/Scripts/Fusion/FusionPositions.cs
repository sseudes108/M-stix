using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionPositions : MonoBehaviour {
    [SerializeField] private List<Transform> _LinePositions;
    [SerializeField] private Transform _resultCardPosition, _boardPlaceSelectionPlace;
    [SerializeField] private Transform _handOffCameraPosition, _defaultHandPosition;

    public Transform HandOffCameraPosition => _handOffCameraPosition;
    public Transform HandDefaultPosition => _defaultHandPosition;
    public Transform ResultCardPosistion => _resultCardPosition;
    private Card _cardInBoardPlaceSelection;
        
    public void MoveCardToPosition(List<Card> cardsToMove){
        var cardIndex = 0;

        foreach(var card in cardsToMove){
            card.MoveCard(_LinePositions[cardIndex]);
            cardIndex++;
        }
    }

    public void MergeCards(List<Card> cardsToMove){
        foreach(var card in cardsToMove){
            card.MoveCard(_resultCardPosition);
        }
    }
    public void MoveCardToResultPosition(Card cardToMove){
        cardToMove.MoveCard(_resultCardPosition);
    }
    public void MoveCardToFirstPositionInlinePos(Card cardToMove){
        cardToMove.MoveCard(_LinePositions[0]);
    }

    public void MoveCardToBoardPlaceSelectionPlace(Card cardToMove){
        cardToMove.MoveCard(_boardPlaceSelectionPlace);
        _cardInBoardPlaceSelection = cardToMove;
    }
    
    public Card GetCardInBoardSelectionPlace(){
        return _cardInBoardPlaceSelection;
    }
}