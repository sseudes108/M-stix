// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FusionPhase : AbstractState{
//     public FusionPhase(StateMachine stateMachine) : base(stateMachine){}

//     public override void Enter(){
//         SubscribeEvents();

//         List<Card> selectedCardList;
//         if(StateMachine.Battle.TurnManager.IsPlayerTurn){
//             selectedCardList = StateMachine.Battle.CardManager.Selector.SelectedList;
//         }else{
//             selectedCardList = StateMachine.AI.Actor.CardSelector.SelectedList;
//         }

//         if(StateMachine.Battle != null){
//             StateMachine.Battle.StartCoroutine(FusionPhaseRoutine(selectedCardList));
//         }else{
//             Debug.LogError("StateMachine.Battle == null");
//         }
//     }

//     public override void Exit(){
//         UnsubscribeEvents();
//     }
    
//     public IEnumerator FusionPhaseRoutine(List<Card> selectedCards){
//         StateMachine.Battle.FusionManager.StartFusionRoutine(selectedCards, StateMachine.Battle.TurnManager.IsPlayerTurn);
//         yield return null;
//     }

//     public override void SubscribeEvents(){
//         StateMachine.Battle.FusionManager.OnFusionEnd.AddListener(FusionManager_OnFusionEnd);
//     }

//     public override void UnsubscribeEvents(){
//         StateMachine.Battle.FusionManager.OnFusionEnd.RemoveListener(FusionManager_OnFusionEnd);
//     }

//     private void FusionManager_OnFusionEnd(Card ResultCard){
//         StateMachine.Battle.ChangeState(StateMachine.Battle.CardStatSelection);
//     }

//     public override string ToString(){ return "Fusion"; }
// }
