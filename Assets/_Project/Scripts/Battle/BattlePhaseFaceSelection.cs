using System.Collections;
using UnityEngine;

public class BattlePhaseFaceSelection : BattleAbstract
{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.FaceSelection);
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
        Debug.Log("Waiting Face Select");
        yield return new WaitForSeconds(3);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.BoardPlaceSelectionPhase);
    }
}