public class BattlePhaseSelections : BattleAbstract{

    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Selections);

        BattleManager.Instance.FusionManager.AfterFusionSelections.StartSelection(BattleManager.Instance.Fusion.GetResultCard());
    }

    public override void ExitState(){

    }

    public override void Update(){

    }
}