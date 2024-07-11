public class BoardPlaceSelectionPhase : AbstractState{   

    // public static Action<Card, bool> OnBoardPlaceSelectionStart;
    // public static Action<Card, bool> OnBoardPlaceSelectionEnd;

    public override void Enter(){
        SubscribeEvents();
        Battle.BattleManager.BoardPlaceSelectionStart(ResultCard, IsPlayerTurn);
        // OnBoardPlaceSelectionStart?.Invoke(ResultCard, IsPlayerTurn);
    }

    public override void Exit(){
        UnsubscribeEvents();
    }

    public override void SubscribeEvents(){
        Battle.BoardManager.OnBoardPlaceSelected.AddListener(BoardManager_OnBoardPlaceSelected);
        // BoardPlace.OnBoardPlaceSelected += BoardPlace_OnBoardPlaceSelected;
    }

    public override void UnsubscribeEvents(){
        Battle.BoardManager.OnBoardPlaceSelected.RemoveListener(BoardManager_OnBoardPlaceSelected);
        // BoardPlace.OnBoardPlaceSelected -= BoardPlace_OnBoardPlaceSelected;
    }

    private void BoardManager_OnBoardPlaceSelected(){
        Battle.BattleManager.BoardPlaceSelectionEnd(ResultCard, IsPlayerTurn);
        // OnBoardPlaceSelectionEnd.Invoke(ResultCard, IsPlayerTurn);
        Battle.ChangeState(Battle.Action);
    }

    public override string ToString(){
        return "Board Place Sel.";
    }
}