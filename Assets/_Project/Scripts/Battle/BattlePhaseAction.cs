using UnityEngine;

public class BattlePhaseAction : BattleAbstract {
    public override void EnterState(){
        Debug.Log("Action Phase");
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Action);
    }

    public override void ExitState(){
        
    }

    public override void Update(){
        
    }
}