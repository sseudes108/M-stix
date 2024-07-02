using System;

public class BattlePhaseBoardPlaceSelection : BattleAbstract{
    private Card _resultCard;

    public static Action<Card> OnBoardPlaceSelection;
    public static Action<Card> AIBoardPlaceSelection;
    public static Action OnBoardSelectionEnd;

    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.BoardPlaceSelection);

        _resultCard = BattleManager.Instance.FusionManager.GetResultCard();

        //Board material color change
        // BattleManager.Instance.BoardPlaceVisuals.HighLightSelectionPhase(_resultCard);

        //Move result card to board place selection
        // BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(_resultCard);

        OnBoardPlaceSelection?.Invoke(_resultCard);

        //AI
        if(!_playerTurn){
            // BattleManager.Instance.AIManager.BoardPlaceSelector.StartBoardPlaceSelection(_resultCard);
            AIBoardPlaceSelection?.Invoke(_resultCard);
        }
    }

    public override void ExitState(){
        OnBoardSelectionEnd?.Invoke();      
        //Board material reset color
        // BattleManager.Instance.BoardPlaceVisuals.ResetPlaceHighlightColor();
        // BattleManager.Instance.CardSelector.ClearSelectedlist();
    }

    public override void Update(){

    }
}