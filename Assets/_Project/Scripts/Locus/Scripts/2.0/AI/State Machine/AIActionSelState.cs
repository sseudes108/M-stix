// using System.Collections;

// public class AIActionSelState : AbstractState{
//     public AIActionSelState(StateMachine stateMachine) : base(stateMachine) {}
//     public override void Enter() { StateMachine.AI.StartCoroutine(AIRoutine()); }

//     public override void Exit(){}

//     public IEnumerator AIRoutine(){
//         yield return StateMachine.Battle.StartCoroutine(StateMachine.AI.Actor.EffectSelector.SelectEffectRoutine());
//         yield return null;
//     }

//     public override string ToString(){
//         return "Action Sel.";
//     }
// }