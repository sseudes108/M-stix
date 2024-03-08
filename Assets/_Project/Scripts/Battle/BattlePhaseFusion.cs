using System.Collections;
using UnityEngine;

public class BattlePhaseFusion : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Fusion);

        var selectedlist = BattleManager.Instance.FusionManager.GetFusionList();

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            //Move hand off camera
            if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
                BattleManager.Instance.PlayerHand.MoveHand(BattleManager.Instance.FusionPositions.HandOffCameraPosition.position);
            }
        }

        foreach(var card in selectedlist){
            card.SetCardOnHand(false);
        }

        BattleManager.Instance.Fusion.StartFusionRoutine(selectedlist);
    }

    public override void ExitState(){

    }

    public override void Update(){
        
    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Debug.Log("Waiting Fusion - Enemy");
        }
        yield return new WaitForSeconds(_waitTime);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.SelectionsPhase);
    }
}