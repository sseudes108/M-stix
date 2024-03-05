using System.Collections;
using UnityEngine;

public class BattlePhaseStart : BattleAbstract{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Start);
        Wait();
    }

    public override void Update(){}
    
    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        Debug.Log("Waiting Start");
        yield return new WaitForSeconds(1);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.DrawPhase);
    }

    public override void ExitState(){
        
    }
}
