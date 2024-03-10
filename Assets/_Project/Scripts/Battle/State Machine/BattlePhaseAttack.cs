using UnityEngine;

public class BattlePhaseAttack : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Attack);
    }

    public override void ExitState(){
        
    }

    public override void Update(){
        
    }
}