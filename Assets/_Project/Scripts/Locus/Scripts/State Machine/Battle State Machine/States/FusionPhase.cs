using System.Collections;
using System.Collections.Generic;

public class FusionPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();

        List<Card> selectedCardList;
        if(Battle.BattleManager.IsPlayerTurn){
            selectedCardList = Battle.CardManager.Selector.SelectedList;
        }else{
            selectedCardList = AI.Actor.CardSelector.SelectedList;
        }

        Battle.StartCoroutine(FusionPhaseRoutine(selectedCardList));
    }

    public override void Exit(){
        UnsubscribeEvents();
    }
    
    public IEnumerator FusionPhaseRoutine(List<Card> selectedCards){
        Battle.FusionManager.StartFusionRoutine(selectedCards, Battle.BattleManager.IsPlayerTurn);
        yield return null;
    }

    public override void SubscribeEvents(){
        Battle.FusionManager.OnFusionEnd.AddListener(FusionManager_OnFusionEnd);
    }

    public override void UnsubscribeEvents(){
        Battle.FusionManager.OnFusionEnd.RemoveListener(FusionManager_OnFusionEnd);
    }

    private void FusionManager_OnFusionEnd(Card ResultCard){
        Battle.ChangeState(Battle.CardStatSelection);
    }

    public override string ToString(){ return "Fusion"; }
}
