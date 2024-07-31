public class BoardPlaceSelectionPhase : AbstractState{
    public BoardPlaceSelectionPhase(StateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){
        SubscribeEvents();
        StateMachine.Battle.BattleManager.BoardPlaceSelectionStart(StateMachine.Battle.FusionManager.ResultCard, StateMachine.Battle.TurnManager.IsPlayerTurn);

        if(!StateMachine.Battle.TurnManager.IsPlayerTurn){
            StateMachine.AI.ChangeState(StateMachine.AI.BoardPlaceSelect);
        }
    }

    public override void Exit(){
        UnsubscribeEvents();
    }

    public override void SubscribeEvents(){
        StateMachine.Battle.BoardManager.OnBoardPlaceSelected.AddListener(BoardManager_OnBoardPlaceSelected);
    }

    public override void UnsubscribeEvents(){
        StateMachine.Battle.BoardManager.OnBoardPlaceSelected.RemoveListener(BoardManager_OnBoardPlaceSelected);
    }

    private void BoardManager_OnBoardPlaceSelected(){
        StateMachine.Battle.BattleManager.BoardPlaceSelectionEnd(StateMachine.Battle.FusionManager.ResultCard, StateMachine.Battle.TurnManager.IsPlayerTurn);
        StateMachine.Battle.ChangeState(StateMachine.Battle.Action);
    }

    public override string ToString(){
        return "Board Place Sel.";
    }
}