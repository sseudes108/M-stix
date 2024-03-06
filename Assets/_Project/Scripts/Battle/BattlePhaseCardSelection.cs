public class BattlePhaseCardSelection : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.CardSelection);
    }

    public override void ExitState(){
        
    }

    public override void Update(){
        
    }

    public void EndSelection(){
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
    }
}