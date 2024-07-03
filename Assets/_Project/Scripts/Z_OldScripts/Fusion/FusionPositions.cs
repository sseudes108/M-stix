// using System.Collections.Generic;
// using UnityEngine;

// public class FusionPositions : MonoBehaviour {
//     private List<Transform> _linePositions;
//     private Transform _resultCardPosition, _boardSelectionPlace;

//     // [SerializeField] private Transform _handOffCameraPosition, _defaultHandPosition;

//     // public Transform HandOffCameraPosition => _handOffCameraPosition;
//     // public Transform HandDefaultPosition => _defaultHandPosition;

//     [Header("Player Positions")]
//     [SerializeField] private List<Transform> _playerFusionLinePositions;
//     [SerializeField] private Transform _playerResultCardPosition, _playerBoardSelectionPlace;

//     [Header("Enemy Positions")]

//     [SerializeField] private List<Transform> _enemyFusionLinePositions;
//     [SerializeField] private Transform _enemyResultCardPosition, _enemyBoardSelectionPlace;



// #region Unity Methods

//     private void OnEnable() {
//         // BPBoardPlaceSelection.OnBoardPlaceSelection += BattlePhaseBoardPlaceSelection_OnBoardPlaceSelection;
//         Fusion.OnFusion += Fusion_OnFusion;
//         Fusion.OnMergeCards += Fusion_OnMergeCards;
//         Fusion.OnFusionFinished += Fusion_OnFusionFinished;
//     }

//     private void OnDisable() {
//         // BPBoardPlaceSelection.OnBoardPlaceSelection += BattlePhaseBoardPlaceSelection_OnBoardPlaceSelection;
//         Fusion.OnFusion -= Fusion_OnFusion;
//         Fusion.OnMergeCards -= Fusion_OnMergeCards;
//         Fusion.OnFusionFinished -= Fusion_OnFusionFinished;
//     }

// #endregion

// #region Events Methods

//     private void Fusion_OnFusion(List<Card> cards){
//         MoveCardToFusionPosition(cards);
//     }

//     private void Fusion_OnMergeCards(List<Card> cards){
//         MergeCards(cards);
//     }

//     // private void BattlePhaseBoardPlaceSelection_OnBoardPlaceSelection(Card card){
//     //     MoveCardToBoardPlaceSelectionPos(card);
//     // }

//     private void Fusion_OnFusionFinished(Card cardToMove, bool isPlayerTurn){
//         MoveCardToResultPosition(cardToMove, isPlayerTurn);
//     }

// #endregion

// #region Costom Methods Methods

//     public void MoveCardToResultPosition(Card cardToMove, bool isPlayerTurn){
//         if(isPlayerTurn){
//             _resultCardPosition = _playerResultCardPosition;
//         }else{
//             _resultCardPosition = _enemyResultCardPosition;
//         }

//         cardToMove.MoveCard(_resultCardPosition);
//     }

//     private void MoveCardToFusionPosition(List<Card> cardsToMove){
//         var cardIndex = 0;

//         if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
//             _linePositions = _playerFusionLinePositions;
//         }else{
//             _linePositions = _enemyFusionLinePositions;
//         }

//         foreach(var card in cardsToMove){
//             card.MoveCard(_linePositions[cardIndex]);
//             cardIndex++;
//         }
//     }

//     public void MergeCards(List<Card> cardsToMove){
//         if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
//             _resultCardPosition = _playerResultCardPosition;
//         }else{
//             _resultCardPosition = _enemyResultCardPosition;
//         }

//         foreach(var card in cardsToMove){
//             card.MoveCard(_resultCardPosition);
//         }
//     }

//     public void MoveCardToFirstPositionInlinePos(Card cardToMove){
//         if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
//             _linePositions = _playerFusionLinePositions;
//         }else{
//             _linePositions = _enemyFusionLinePositions;
//         }

//         cardToMove.MoveCard(_linePositions[0]);
//     }

//     public void MoveCardToBoardPlaceSelectionPos(Card cardToMove){
//         if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
//             Debug.Log("Player Turn");
//             _boardSelectionPlace = _playerBoardSelectionPlace;
//         }else{
//             _boardSelectionPlace = _enemyBoardSelectionPlace;
//         }
        
//         cardToMove.MoveCard(_boardSelectionPlace);
//     }

// #endregion

// }