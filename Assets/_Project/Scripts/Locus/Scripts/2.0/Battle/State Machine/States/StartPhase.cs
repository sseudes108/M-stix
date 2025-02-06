// using System.Collections;
// using UnityEngine;

// public class StartPhase : AbstractState{
//     public StartPhase(StateMachine stateMachine) : base(stateMachine){}
//     public override void Enter(){
//         if(StateMachine.Battle != null){
//             StateMachine.Battle.StartCoroutine(BattlePhaseStartRoutine());
//         }
//     }

//     public override void Exit(){}
    
//     public IEnumerator BattlePhaseStartRoutine(){
//         yield return new WaitForSeconds(1f);
//         StateMachine.Battle.BattleManager.StartPhase();

//         yield return new WaitForSeconds(2f);
//         StateMachine.Battle.ChangeState(StateMachine.Battle.DrawPhase);

//         yield return null;
//     }

//     public override string ToString(){ return "Start"; }
// }