// public class EndPhase : AbstractState{
//     public EndPhase(StateMachine stateMachine) : base(stateMachine){}

//     public override void Enter(){
//         StateMachine.Battle.StartCoroutine(StateMachine.Battle.BattleManager.ChangeStateRoutine(3f, StateMachine.Battle, StateMachine.Battle.StartPhase));
//     }

//     public override void Exit(){
//         StateMachine.Battle.TurnManager.EndTurn();
//     }

//     public override string ToString(){
//         return "End Phase";
//     }
// }