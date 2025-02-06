// using System.Collections;
// using UnityEngine;

// public class AISelectCardState : AbstractState{
//     public AISelectCardState(StateMachine stateMachine) : base(stateMachine) {}

//     public override void Enter() { StateMachine.StartCoroutine(AIRoutine()); }

//     public override void Exit(){}

//     public IEnumerator AIRoutine(){
//         StateMachine.AI.Actor.OrganizeCardLists(StateMachine.AI.Actor.CardOrganizer.CardsInAIHand);
//         yield return new WaitForSeconds(0.5f);
//         yield return StateMachine.StartCoroutine(StateMachine.AI.Actor.CardSelector.SelectCardRoutine());
//         yield return null;
//     }

//     public override string ToString(){
//         return "Select Card";
//     }
// }