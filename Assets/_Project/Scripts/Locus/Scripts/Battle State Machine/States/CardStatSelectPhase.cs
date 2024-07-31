public class CardStatSelectPhase : AbstractState{
    public CardStatSelectPhase(StateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){
        SubscribeEvents();
        StateMachine.Battle.BattleManager.StatSelectStart(StateMachine.Battle.FusionManager.ResultCard);

        if(!StateMachine.Battle.TurnManager.IsPlayerTurn){
            StateMachine.AI.ChangeState(StateMachine.AI.CardStatSelect);
        }
    }

    public override void Exit(){
        UnsubscribeEvents();
        StateMachine.Battle.BattleManager.StatSelectEnd(StateMachine.Battle.FusionManager.ResultCard, StateMachine.Battle.TurnManager.IsPlayerTurn);
    }

    public override void SubscribeEvents(){
        if(StateMachine.Battle.TurnManager.IsPlayerTurn){
            StateMachine.Battle.CardStatSelManager.OnSelectionsEnd.AddListener(CardStatSelManager_OnSelectionsEnd);
            return;
        }

        StateMachine.AI.Actor.CardStatSelector_OnCardStatSelectionFinished.AddListener(AI_Actor_CardStatSelectior_OnCardStatSelectionFinished);
    }

    public override void UnsubscribeEvents(){
        if(StateMachine.Battle.TurnManager.IsPlayerTurn){
            StateMachine.Battle.CardStatSelManager.OnSelectionsEnd.RemoveListener(CardStatSelManager_OnSelectionsEnd);
            return;
        }

        StateMachine.AI.Actor.CardStatSelector_OnCardStatSelectionFinished.AddListener(AI_Actor_CardStatSelectior_OnCardStatSelectionFinished);
    }

    private void CardStatSelManager_OnSelectionsEnd() { ChangePhase(); }
    private void AI_Actor_CardStatSelectior_OnCardStatSelectionFinished() { ChangePhase(); }
    private void ChangePhase() { StateMachine.Battle.ChangeState(StateMachine.Battle.BoardPlaceSelection); }

    public override string ToString() { return "Card Stats Sel."; }
}