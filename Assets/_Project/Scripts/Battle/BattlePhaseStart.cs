using System.Collections;
using UnityEngine;

public class BattlePhaseStart : BattleAbstract{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Start);
        Wait();
    }

    public override void Update(){}
    

    public override void ExitState(){
        
    }
    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        Debug.Log("Waiting Start");
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.BoardPlaceVisuals.LightUpBoard();
        
        yield return new WaitForSeconds(0.5f);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.DrawPhase);
    }
}
