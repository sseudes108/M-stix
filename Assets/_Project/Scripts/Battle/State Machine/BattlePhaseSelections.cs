using UnityEngine;

public class BattlePhaseSelections : BattleAbstract{

    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Selections);
        
        BattleManager.Instance.ActionsManager.AfterFusionSelections.StartSelection();
    }

    public override void ExitState(){

    }

    public override void Update(){

    }
}