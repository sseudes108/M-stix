public class ActionPhase : AbstractState{
    public ActionPhase(StateMachine controller) : base(controller){}

    public override void Enter(){
        StateMachine.BattleManager.ActionPhaseStart();
    }

    public override void Exit() {}

    public override string ToString(){
        return "Action Phase";
    }
}