// using System.Collections.Generic;
// using UnityEngine;

// public class FusionPositions: Fusion {
//     private List<Transform> _linePositions;
//     private Transform _resultCardPosition, _boardSelectionPlace;

//     [Header("Player")]
//     [SerializeField] private List<Transform> _playerFusionPositions;
//     [SerializeField] private Transform _playerResultCardPosition, _playerBoardSelectionPlace;
    
//     [Header("Enemy")]
//     [SerializeField] private List<Transform> _enemyFusionPositions;
//     [SerializeField] private Transform _enemyResultCardPosition, _enemyBoardSelectionPlace;


//     private void OnEnable() { 
//         _battleManager.OnBoardPlaceSelectionStart.AddListener(BattleManager_OnBoardPlaceSelectionStart);
//     }

//     private void OnDisable() { 
//         _battleManager.OnBoardPlaceSelectionStart.RemoveListener(BattleManager_OnBoardPlaceSelectionStart);
//     }

//     private void Start() {
//         _fusionManager.SetPositions(this);
//     }

//     private void BattleManager_OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn) {
//         MoveToBoardPlaceSelection(card, isPlayerTurn); 
//     }

//     private void MoveToBoardPlaceSelection(Card card, bool isPlayerTurn){
//         if(card == null){
//             Debug.Log("Card is Null in FusionPositions at MoveToBoardPlaceSelection()");
//         }

//         if(isPlayerTurn){
//             _boardSelectionPlace = _playerBoardSelectionPlace;
//         }else{
//             _boardSelectionPlace = _enemyBoardSelectionPlace;
//         }

//         card.MoveCard(_boardSelectionPlace);
//     }

//     public void MoveCardToResultPosition(Card card, bool isPlayerTurn){
//         if(isPlayerTurn){
//             _resultCardPosition = _playerResultCardPosition;
//         }else{
//             _resultCardPosition = _enemyResultCardPosition;
//         }

//         card.MoveCard(_resultCardPosition);
//     }

//     public void MoveCardsToMergePosition(List<Card> cards, bool isPlayerTurn){
//         foreach(var card in cards){
//             MoveCardToResultPosition(card, isPlayerTurn);
//         }
//     }

//     public void MoveCardsToFusionPosition(List<Card> cards, bool isPlayerTurn){
//         var cardIndex = 0;

//         if(isPlayerTurn){
//             _linePositions = _playerFusionPositions;
//         }else{
//             _linePositions = _enemyFusionPositions;
//         }

//         foreach(var card in cards){
//             card.MoveCard(_linePositions[cardIndex]);
//             card.Visuals.Border.ResetBorderColor();
//             cardIndex++;
//         }
//     }
// }