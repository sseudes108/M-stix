using System.Collections;
using UnityEngine;

public class BattlePhaseBoardPlaceSelection : BattleAbstract{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.BoardPlaceSelection);

        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Wait();
        }else{
            //Board material color change
            BattleManager.Instance.BoardPlaceVisuals.BoarderSelectionPhaseHighlight(BattleManager.Instance.Fusion.GetResultCard(), 3f);

            //Move result card to board place selection
            BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(BattleManager.Instance.Fusion.GetResultCard());
        }
       
        // //Board material color change
        // BattleManager.Instance.BoardPlaceVisuals.BoarderSelectionPhaseHighlight(BattleManager.Instance.Fusion.GetResultCard(), 3f);

        // //Move result card to board place selection
        // BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(BattleManager.Instance.Fusion.GetResultCard());
    }

    public override void ExitState(){
        //Board material reset color
        BattleManager.Instance.BoardPlaceVisuals.ResetPlaceHighlightColor(BattleManager.Instance.Fusion.GetResultCard(), 1.5f);
    }

    public override void Update(){

    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Debug.Log("Waiting Selection - Enemy");
        }
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.ActionPhase);
    }
}