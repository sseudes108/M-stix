using System.Collections;

public class CardSelectionPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();
        Battle.BattleManager.CardSelectionStart(); // Unlock card selection
        
        if(!Battle.TurnManager.IsPlayerTurn){
            Battle.StartCoroutine(AIRoutine());
        }
    }
    
    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.CardSelectionEnd();// lock card selection
    }

    public override void SubscribeEvents(){
        if(Battle.TurnManager.IsPlayerTurn){
            Battle.UIManager.OnCardSelectionFinished.AddListener(UIManager_OnCardSelectionFinished);
            return;
        }

        AI.Actor.CardSelector_OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
    }

    public override void UnsubscribeEvents(){
        if(Battle.TurnManager.IsPlayerTurn){
            Battle.UIManager.OnCardSelectionFinished.RemoveListener(UIManager_OnCardSelectionFinished);
            return;
        }
        
        AI.Actor.CardSelector_OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
    }

    private void UIManager_OnCardSelectionFinished() { ChangePhase(); }
    private void AI_Actor_CardSelector_OnCardsSelected() { ChangePhase(); }
    private void ChangePhase(){ Battle.ChangeState(Battle.Fusion); }

    public IEnumerator AIRoutine(){
        yield return Battle.StartCoroutine(AI.Actor.CardSelector.SelectCardRoutine(AI.Manager.CardsInHand));
        yield return null;
    }

    public override string ToString(){ return "Card Sel."; }
}