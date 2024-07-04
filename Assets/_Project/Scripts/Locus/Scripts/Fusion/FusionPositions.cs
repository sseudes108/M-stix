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
    

    private void OnEnable() {
        FusionPhase.OnStartFusion += FusionPhase_OnStartFusion;
        Fusion.OnMergeCards += Fusion_OnMergeCards;
        Fusion.OnFusionFinished += FusionPhase_OnFusionFinished;
    }

    private void OnDisable() {
        FusionPhase.OnStartFusion -= FusionPhase_OnStartFusion;
        Fusion.OnMergeCards -= Fusion_OnMergeCards;
        Fusion.OnFusionFinished -= FusionPhase_OnFusionFinished;
    }

    private void FusionPhase_OnFusionFinished(Card card, bool isPlayerTurn){
        if(isPlayerTurn){
            _resultCardPosition = _playerResultCardPosition;
        }else{
            _resultCardPosition = _enemyResultCardPosition;
        }

        card.MoveCard(_playerResultCardPosition);
    }

    private void Fusion_OnMergeCards(List<Card> cards, bool isPlayerTurn){
        if(isPlayerTurn){
            _resultCardPosition = _playerResultCardPosition;
        }else{
            _resultCardPosition = _enemyResultCardPosition;
        }

        foreach(var card in cards){
            card.MoveCard(_resultCardPosition);
        }
        // MergeCards(cards, isPlayerTurn);
    }

    // public void MergeCards(List<Card> cardsToMove, bool isPlayerTurn){
    //     if(isPlayerTurn){
    //         _resultCardPosition = _playerResultCardPosition;
    //     }else{
    //         _resultCardPosition = _enemyResultCardPosition;
    //     }

    //     foreach(var card in cardsToMove){
    //         card.MoveCard(_resultCardPosition);
    //     }
    // }

    private void FusionPhase_OnStartFusion(List<Card> cards, bool isPlayerTurn){
        MoveCardToFusionPosition(cards, isPlayerTurn);
    }

    private void MoveCardToFusionPosition(List<Card> cards, bool isPlayerTurn){
        var cardIndex = 0;

        if(isPlayerTurn){
            _linePositions = _playerFusionPositions;
        }else{
            _linePositions = _enemyFusionPositions;
        }

        foreach(var card in cards){
            card.MoveCard(_linePositions[cardIndex]);
            card.CardVisual.Shader.ResetBorderColor();
            cardIndex++;
        }
    }
}