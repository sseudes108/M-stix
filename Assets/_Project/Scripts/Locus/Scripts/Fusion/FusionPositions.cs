using System;
using System.Collections.Generic;
using UnityEngine;

public class FusionPositions : MonoBehaviour {
    private List<Transform> _linePositions;
    private Transform _resultCardPosition, _boardSelectionPlace;

    [SerializeField] private List<Transform> _playerFusionPositions;
    [SerializeField] private Transform _playerResultCardPosition, _playerBoardSelectionPlace;
    
    [SerializeField] private List<Transform> _enemyFusionPositions;
    [SerializeField] private Transform _enemyResultCardPosition, _enemyBoardSelectionPlace;
    
    private void Fusion_OnFusionEnd(Card card, bool isPlayerTurn){
        MoveCardToBoardPlaceSelectionSpot(card, isPlayerTurn);
    }

    private void MoveCardToBoardPlaceSelectionSpot(Card card, bool isPlayerTurn){
        if(isPlayerTurn){
            _boardSelectionPlace = _playerBoardSelectionPlace;
        }else{
            _boardSelectionPlace = _enemyBoardSelectionPlace;
        }

        card.MoveCard(_boardSelectionPlace);
    }

    public void MoveCardToResultPosition(Card card, bool isPlayerTurn){
        if(isPlayerTurn){
            _resultCardPosition = _playerResultCardPosition;
        }else{
            _resultCardPosition = _enemyResultCardPosition;
        }

        card.MoveCard(_resultCardPosition);
    }

    public void MoveCardsToMergePosition(List<Card> cards, bool isPlayerTurn){
        foreach(var card in cards){
            MoveCardToResultPosition(card, isPlayerTurn);
        }
    }

    public void MoveCardToFusionPosition(List<Card> cards, bool isPlayerTurn){
        var cardIndex = 0;

        if(isPlayerTurn){
            _linePositions = _playerFusionPositions;
        }else{
            _linePositions = _enemyFusionPositions;
        }

        foreach(var card in cards){
            card.MoveCard(_linePositions[cardIndex]);
            card.CardVisual.Border.ResetBorderColor();
            cardIndex++;
        }
    }
}