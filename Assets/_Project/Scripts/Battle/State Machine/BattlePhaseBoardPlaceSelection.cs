public class BattlePhaseBoardPlaceSelection : BattleAbstract{
    Card _resultCard;
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.BoardPlaceSelection);

        _resultCard = BattleManager.Instance.FusionManager.GetResultCard();

        //Board material color change
        BattleManager.Instance.BoardPlaceVisuals.HighLightSelectionPhase(_resultCard);

        //Move result card to board place selection
        BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(_resultCard);

        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.AIManager.BoardPlaceSelector.StartBoardPlaceSelection(_resultCard);
        }
    }

    public override void ExitState(){       
        //Board material reset color
        BattleManager.Instance.BoardPlaceVisuals.ResetPlaceHighlightColor();
        BattleManager.Instance.CardSelector.ClearSelectedlist();
    }

    public override void Update(){

    }
}