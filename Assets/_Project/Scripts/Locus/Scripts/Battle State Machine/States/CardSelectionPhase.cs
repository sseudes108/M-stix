public class CardSelectionPhase : AbstractState{
    public CardSelectionPhase(StateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){
        SubscribeEvents();
        StateMachine.BattleManager.CardSelectionStart(); // Unlock card selection
        
        if(!StateMachine.TurnManager.IsPlayerTurn){
            StateMachine.AI.ChangeState(StateMachine.AI.CardSelect);
        }
    }
    
    public override void Exit(){
        UnsubscribeEvents();
        StateMachine.BattleManager.CardSelectionEnd();// lock card selection
    }

    public override void SubscribeEvents(){
        if(StateMachine.TurnManager.IsPlayerTurn){
            StateMachine.UIManager.OnCardSelectionFinished.AddListener(UIManager_OnCardSelectionFinished);
            return;
        }

        StateMachine.AI.Actor.CardSelector_OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
    }

    public override void UnsubscribeEvents(){
        if(StateMachine.TurnManager.IsPlayerTurn){
            StateMachine.UIManager.OnCardSelectionFinished.RemoveListener(UIManager_OnCardSelectionFinished);
            return;
        }
        
        StateMachine.AI.Actor.CardSelector_OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
    }

    private void UIManager_OnCardSelectionFinished() { ChangePhase(); }
    private void AI_Actor_CardSelector_OnCardsSelected() { ChangePhase(); }
    private void ChangePhase(){ StateMachine.Battle.ChangeState(StateMachine.Battle.Fusion); }

    public override string ToString(){ return "Card Sel."; }
}