public class BattlePhaseBoardPlaceSelection : BattleAbstract
{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.BoardPlaceSelection);
       
        //Board material color change
        BattleManager.Instance.BoardPlaceVisuals.BoarderSelectionPhaseHighlight(BattleManager.Instance.Fusion.GetResultCard(), 10f);

        //Move result card to board place selection
        BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(BattleManager.Instance.Fusion.GetResultCard());
    }

    public override void ExitState(){
        //Board material reset color
        BattleManager.Instance.BoardPlaceVisuals.BoarderSelectionPhaseHighlight(BattleManager.Instance.Fusion.GetResultCard(), 1.5f);
    }

    public override void Update(){

    }
}