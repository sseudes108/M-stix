public class CardSelectionPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();
        Battle.BattleManager.CardSelectionStart(); // Unlock card selection
    }
    
    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.CardSelectionEnd();// lock card selection
    }

    public override void SubscribeEvents(){
        Battle.UIManager.OnCardSelectionFinished.AddListener(UIManager_OnCardSelectionFinished);
    }

    public override void UnsubscribeEvents(){
        Battle.UIManager.OnCardSelectionFinished.RemoveListener(UIManager_OnCardSelectionFinished);
    }

    private void UIManager_OnCardSelectionFinished(){
        Battle.ChangeState(Battle.Fusion);
    }

    public override string ToString(){ return "Card Sel."; }
}