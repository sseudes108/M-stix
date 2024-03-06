using System.Collections;
using UnityEngine;

public class BattlePhaseSelectAnima : BattleAbstract
{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.AnimaSelection);
        
        Wait();
    }

    public override void ExitState(){
    }

    public override void Update(){
        
    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        Debug.Log("Waiting Anima Select");
        yield return new WaitForSeconds(3);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.MonsterModeSelectionPhase);
    }
}