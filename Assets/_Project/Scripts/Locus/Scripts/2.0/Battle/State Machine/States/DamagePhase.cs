// public class DamagePhase : AbstractState{
//     public DamagePhase(StateMachine stateMachine) : base(stateMachine){}

//     public override void Enter(){
//         if(StateMachine.TurnManager.IsPlayerTurn){
//             StateMachine.Battle.BattleLogic.SetMonstersToBattle(StateMachine.BattleManager.AttackerMonster, StateMachine.BattleManager.TargetMonster);
//         }else{
//             StateMachine.Battle.BattleLogic.SetMonstersToBattle(StateMachine.AI.Actor.AttackerMonster, StateMachine.AI.Actor.TargetMonster);
//         }

//         StateMachine.Battle.BattleLogic.StartBattleRoutine();
//     }

//     public override void Exit(){}
    
//     public override string ToString(){
//         return "Damage Phase";
//     }
// }