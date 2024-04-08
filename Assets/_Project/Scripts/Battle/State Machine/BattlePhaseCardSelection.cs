using System.Collections;
using UnityEngine;

public class BattlePhaseCardSelection : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.CardSelection);
        
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.UIBattleManager.EndSelectionButton();
        }else{
            BattleManager.Instance.AIManager.ChangeState(BattleManager.Instance.AICardSelection);
        }
    }

    public override void ExitState(){
        
    }

    public override void Update(){
        
    }

    public void EndSelection(){
        BattleManager.Instance.UIBattleManager.DisableEndSelectionButton();
        BattleManager.Instance.FusionManager.SetFusionList();
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
    }
}