using System.Collections;
using UnityEngine;

public class BattlePhaseMonsterModeSelection : BattleAbstract
{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.MonsterModeSelection);
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
        Debug.Log("Waiting Monster Mode Selection");
        yield return new WaitForSeconds(1);
        
        if(BattleManager.Instance.Fusion.GetResultCard().IsFusioned()){
            BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.BoardPlaceSelectionPhase);
        }else{
            BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FaceSelectionPhase);
        }
    }
}