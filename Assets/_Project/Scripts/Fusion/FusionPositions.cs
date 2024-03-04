using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionPositions : MonoBehaviour {
    [SerializeField] private List<Transform> _LinePositions;
    [SerializeField] private Transform _resultCardPosition;
    [SerializeField] private Transform _handOffCameraPosition, _defaultHandPosition;

    public Transform HandOffCameraPos => _handOffCameraPosition;
    public Transform HandDefaultPos => _defaultHandPosition;

    public Transform ResultCardPosition => _resultCardPosition;

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
    public void FusionFailed(Card cardToMove){
        cardToMove.MoveCard(_resultCardPosition);
    }
    public void MoveCardToFirstPositionInlinePos(Card cardToMove){
        cardToMove.MoveCard(_LinePositions[0]);
    }
    
    public Transform ResultCardPos => _resultCardPosition;
}