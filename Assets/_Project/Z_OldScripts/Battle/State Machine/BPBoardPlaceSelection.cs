// using System;
// using System.Collections;
// using UnityEngine;

// public class BPBoardPlaceSelection : AbstractBattleState{
//     private Card _resultCard;

//     public static Action<Card> OnBoardPlaceSelectionStart;
//     // public static Action<Card> AIBoardPlaceSelection;
//     public static Action OnBoardPlaceSelectionEnd;

//     public override void EnterState(){
//         OnEnterState?.Invoke(this);

//         // OnBoardPlaceSelectionStart?.Invoke(BattleManager.Instance.FusionManager.GetResultCard());

//         // BattleManager.Instance.StartCoroutine(BoardPlaceRoutine());

//         _resultCard = BattleManager.Instance.FusionManager.GetResultCard();

//         //Board material color change
//         BattleManager.Instance.BoardPlaceVisuals.HighLightSelectionPhase(_resultCard);

//         //Move result card to board place selection
//         BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(_resultCard);

//         //AI
//         if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
//             BattleManager.Instance.AIStateManager.BoardPlaceSelector.StartBoardPlaceSelection(_resultCard);
//             // AIBoardPlaceSelection?.Invoke(_resultCard);
//         }
//     }

//     public override void ExitState(){
//         Debug.Log("T");
//         OnBoardPlaceSelectionEnd?.Invoke();
//         //Board material reset color
//         // BattleManager.Instance.BoardPlaceVisuals.ResetPlaceHighlightColor();
//         // BattleManager.Instance.CardSelector.ClearSelectedlist();
//     }

//     public override void Update(){

//     }

//     // private IEnumerator BoardPlaceRoutine(){
//     //     _resultCard = BattleManager.Instance.FusionManager.GetResultCard();
//     //     yield return null;

//     //     OnBoardPlaceSelection?.Invoke(_resultCard);
//     //     yield return null;

//     //     BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(_resultCard);

//     //     if(!_isPlayerTurn){
//     //         BattleManager.Instance.AIStateManager.BoardPlaceSelector.StartBoardPlaceSelection(_resultCard);
//     //         AIBoardPlaceSelection?.Invoke(_resultCard);
//     //     }
//     //     yield return null;
//     // }
// }