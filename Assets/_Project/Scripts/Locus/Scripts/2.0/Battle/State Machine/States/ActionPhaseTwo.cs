// using UnityEngine;

// public class ActionPhaseTwo : AbstractState{
//     public ActionPhaseTwo(StateMachine stateMachine) : base(stateMachine){}

//     public override void Enter(){
//         Debug.Log("ActionPhaseTwo - Enter()");
//         StateMachine.Battle.StartCoroutine(StateMachine.Battle.BattleManager.ChangeStateRoutine(2f, StateMachine.Battle, StateMachine.Battle.EndPhase));
//     }

//     public override void Exit(){}
    
//     public override string ToString(){
//         return "Action Two";
//     }
// }