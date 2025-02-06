// using System.Collections;

// public class AIBoardPlaceSelState : AbstractState{
//     public AIBoardPlaceSelState(StateMachine stateMachine) : base(stateMachine){}

//     public override void Enter() { StateMachine.AI.StartCoroutine(AIRoutine(StateMachine.AI.Actor.GetFusionedCard())); }

//     public override void Exit(){}

//     public IEnumerator AIRoutine(Card cardToPlace){
//         yield return StateMachine.Battle.StartCoroutine(StateMachine.AI.Actor.BoardPlaceSelector.BoardSelectionRoutine(cardToPlace));
//         yield return null;
//     }

//     public override string ToString(){
//         return "Board Place Sel.";
//     }
// }