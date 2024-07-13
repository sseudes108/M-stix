using System.Diagnostics;

public class CardSelectionPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();
        Battle.BattleManager.CardSelectionStart(); // Unlock card selection
        
        if(!IsPlayerTurn){
            AI.Actor.CardSelector.SelectRandomCard(AI.Manager.CardsInHand);
        }
    }
    
    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.CardSelectionEnd();// lock card selection
    }

    public override void SubscribeEvents(){
        if(IsPlayerTurn){
            Battle.UIManager.OnCardSelectionFinished.AddListener(UIManager_OnCardSelectionFinished);
        }else{
            AI.Actor.OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
        }
    }

    public override void UnsubscribeEvents(){
        if(IsPlayerTurn){
            Battle.UIManager.OnCardSelectionFinished.RemoveListener(UIManager_OnCardSelectionFinished);
        }else{
            AI.Actor.OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
        }
    }

    private void UIManager_OnCardSelectionFinished(){
        Battle.ChangeState(Battle.Fusion);
    }

    private void AI_Actor_CardSelector_OnCardsSelected(){
        Battle.ChangeState(Battle.Fusion);
    }

    public override string ToString(){ return "Card Sel."; }
}