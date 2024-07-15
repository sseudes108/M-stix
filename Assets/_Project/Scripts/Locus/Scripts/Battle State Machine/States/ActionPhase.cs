public class ActionPhase : AbstractState{

    public override void Enter(){
        Battle.BattleManager.ActionPhaseStart();
    }

    public override void Exit() {}

    public override string ToString(){
        return "Action Phase";
    }
}