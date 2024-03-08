using System.Collections.Generic;
using UnityEngine;

public class FusionPositions : MonoBehaviour {
    private List<Transform> _linePositions;
    private Transform _resultCardPosition, _boardSelectionPlace;
    [SerializeField] private Transform _handOffCameraPosition, _defaultHandPosition;

    public Transform HandOffCameraPosition => _handOffCameraPosition;
    public Transform HandDefaultPosition => _defaultHandPosition;
    
    public Transform ResultCardPosistion(){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            _resultCardPosition = _playerResultCardPosition;
        }else{
            _resultCardPosition = _enemyResultCardPosition;
        }

        return _resultCardPosition;
    }

    [Header("Player Positions")]
    [SerializeField] private List<Transform> _playerFusionLinePositions;
    [SerializeField] private Transform _playerResultCardPosition, _playerBoardSelectionPlace;

    [Header("Enemy Positions")]

    [SerializeField] private List<Transform> _enemyFusionLinePositions;
    [SerializeField] private Transform _enemyResultCardPosition, _enemyBoardSelectionPlace;


    public void MoveCardToPosition(List<Card> cardsToMove){
        var cardIndex = 0;

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            _linePositions = _playerFusionLinePositions;
        }else{
            _linePositions = _enemyFusionLinePositions;
        }

        foreach(var card in cardsToMove){
            card.MoveCard(_linePositions[cardIndex]);
            cardIndex++;
        }
    }

    public void MergeCards(List<Card> cardsToMove){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            _resultCardPosition = _playerResultCardPosition;
        }else{
            _resultCardPosition = _enemyResultCardPosition;
        }

        foreach(var card in cardsToMove){
            card.MoveCard(_resultCardPosition);
        }
    }

    public void MoveCardToResultPosition(Card cardToMove){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            _resultCardPosition = _playerResultCardPosition;
        }else{
            _resultCardPosition = _enemyResultCardPosition;
        }

        cardToMove.MoveCard(_resultCardPosition);
    }
    public void MoveCardToFirstPositionInlinePos(Card cardToMove){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            _linePositions = _playerFusionLinePositions;
        }else{
            _linePositions = _enemyFusionLinePositions;
        }

        cardToMove.MoveCard(_linePositions[0]);
    }

    public void MoveCardToBoardPlaceSelectionPos(Card cardToMove){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            _boardSelectionPlace = _playerBoardSelectionPlace;
        }else{
            _boardSelectionPlace = _enemyBoardSelectionPlace;
        }
        
        cardToMove.MoveCard(_boardSelectionPlace);
    }
}