using System.Collections.Generic;
using UnityEngine;

public class FusionPositions : MonoBehaviour {
    private List<Transform> _linePositions;
    [SerializeField] private List<Transform> _playerFusionPositions;
    [SerializeField] private List<Transform> _enemyFusionPositions;

    private void OnEnable() {
        FusionPhase.OnStartFusion += FusionPhase_OnMoveCardsToPosition;
    }

    private void OnDisable() {
        FusionPhase.OnStartFusion -= FusionPhase_OnMoveCardsToPosition;
    }

    private void FusionPhase_OnMoveCardsToPosition(List<Card> cards, bool isPlayerTurn){
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