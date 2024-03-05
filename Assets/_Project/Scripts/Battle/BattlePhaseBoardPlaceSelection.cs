using UnityEngine;

public class BattlePhaseBoardPlaceSelection : BattleAbstract
{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.BoardPlaceSelection);
       
        //Board material color change
        BattleManager.Instance.BoardPlaceVisuals.Fusion_OnFusionEnd(BattleManager.Instance.Fusion.GetResultCard());

        //Move result card to board place selection
        BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(BattleManager.Instance.Fusion.GetResultCard());
    }

    public override void ExitState(){

    }

    public override void Update(){

    }
}