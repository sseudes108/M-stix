using System.Collections.Generic;
using UnityEngine;

public class FusionPositions : MonoBehaviour {
    [SerializeField] private List<Transform> _LinePositions;
    [SerializeField] private Transform _resultCardPosition;
    [SerializeField] private Transform _handOffCameraPosition, _defaultHandPosition;

    public Transform HandOffCameraPos => _handOffCameraPosition;
    public Transform HandDefaultPos => _defaultHandPosition;

    public Transform ResultCardPosition => _resultCardPosition;

    public void MoveCardsToPosition(List<Card> cardsToMove){
        var cardIndex = 0;

        foreach(var card in cardsToMove){
            card.transform.SetParent(_LinePositions[cardIndex]);
            card.MoveCard(_LinePositions[cardIndex].position, _LinePositions[cardIndex].rotation);
            cardIndex++;
        }
    }

    public void FusionSucces_MoveCardMaterials(List<Card> cardsToMove){
        foreach(var card in cardsToMove){
            card.transform.SetParent(_resultCardPosition);
            card.MoveCard(_resultCardPosition.position, _resultCardPosition.rotation);
        }
    }
}