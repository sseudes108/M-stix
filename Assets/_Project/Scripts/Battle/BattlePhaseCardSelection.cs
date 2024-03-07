using System.Collections;
using UnityEngine;

public class BattlePhaseCardSelection : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.CardSelection);
        
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Wait();
        }
    }

    public override void ExitState(){
        
    }

    public override void Update(){
        
    }

    public void EndSelection(){
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Debug.Log("Waiting Start - Enemy");
        }
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
    }
}