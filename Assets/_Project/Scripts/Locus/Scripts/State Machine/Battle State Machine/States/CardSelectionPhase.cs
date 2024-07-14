using System.Collections;
using UnityEngine;

public class CardSelectionPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();
        Battle.BattleManager.CardSelectionStart(); // Unlock card selection
        
        if(!Battle.BattleManager.IsPlayerTurn){
            // AI.Actor.CardSelector.SelectRandomCard(AI.Manager.CardsInHand);
            Battle.StartCoroutine(AiRoutine(3f));
        }
    }
    
    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.CardSelectionEnd();// lock card selection
    }

    public override void SubscribeEvents(){
        if(Battle.BattleManager.IsPlayerTurn){
            Battle.UIManager.OnCardSelectionFinished.AddListener(UIManager_OnCardSelectionFinished);
        }else{
            AI.Actor.CardSelector_OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
        }
    }

    public override void UnsubscribeEvents(){
        if(Battle.BattleManager.IsPlayerTurn){
            Battle.UIManager.OnCardSelectionFinished.RemoveListener(UIManager_OnCardSelectionFinished);
        }else{
            AI.Actor.CardSelector_OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
        }
    }

    private void UIManager_OnCardSelectionFinished(){
        Battle.ChangeState(Battle.Fusion);
    }

    private void AI_Actor_CardSelector_OnCardsSelected(){
        Battle.ChangeState(Battle.Fusion);
    }

    public IEnumerator AiRoutine(float wait){
        yield return new WaitForSeconds(wait);
        Debug.Log("Picking Card");
        Battle.StartCoroutine(AI.Actor.CardSelector.SelectCardRoutine(AI.Manager.CardsInHand));
        // AI.Actor.CardSelector.SelectRandomCard(AI.Manager.CardsInHand);
        yield return null;
    }

    public override string ToString(){ return "Card Sel."; }
}