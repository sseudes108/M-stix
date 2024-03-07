using System.Collections;
using UnityEngine;

public class BattlePhaseCardSelection : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.CardSelection);
        
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            //Mudar stado da AI
            Wait();
        }
    }

    public override void ExitState(){
        
    }

    public override void Update(){
        
    }

    public void EndSelection(){
        BattleManager.Instance.FusionManager.SetFusionList();
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        Debug.Log("Waiting Start - Enemy");
        yield return new WaitForSeconds(_waitTime);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
    }
}