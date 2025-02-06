// public class ActionPhase : AbstractState{
//     public ActionPhase(StateMachine controller) : base(controller){}

//     public override void Enter(){
//         SubscribeEvents();
//         StateMachine.BattleManager.ActionPhaseStart();

//         if(!StateMachine.TurnManager.IsPlayerTurn){
//             StateMachine.AI.ChangeState(StateMachine.AI.ActionSelect);
//         }
//     }

//     public override void Exit() {
//         UnsubscribeEvents();
//     }

//     public override void SubscribeEvents(){
//         StateMachine.AI.Actor.ActionPhaseEnd.AddListener(ActionPhaseEnd);
//     }

//     public override void UnsubscribeEvents(){
//         StateMachine.AI.Actor.ActionPhaseEnd.RemoveListener(ActionPhaseEnd);
//     }

//     private void ActionPhaseEnd(){
//         StateMachine.Battle.ChangeState(StateMachine.Battle.EndPhase);
//     }

//     public override string ToString(){
//         return "Action Phase";
//     }

// }