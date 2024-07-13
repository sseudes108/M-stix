using System.Collections;
using System.Collections.Generic;

public class FusionPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();
        // var selectedCard = GameManager.Instance.CardManager.Selector.SelectedList;

        List<Card> selectedCardList;
        if(IsPlayerTurn){
            selectedCardList = Battle.CardManager.Selector.SelectedList;
        }else{
            selectedCardList = AI.Actor.CardSelector.SelectedList;
        }

        if(Battle!= null){
            Battle.StartCoroutine(FusionPhaseRoutine(selectedCardList));
        }
    }

    public override void Exit(){
        UnsubscribeEvents();
    }
    
    public IEnumerator FusionPhaseRoutine(List<Card> selectedCards){
        GameManager.Instance.Fusion.Fusion.StartFusionRoutine(selectedCards, IsPlayerTurn);
        yield return null;
    }

    public override void SubscribeEvents(){
        Battle.FusionManager.OnFusionEnd.AddListener(FusionManager_OnFusionEnd);
    }

    public override void UnsubscribeEvents(){
        Battle.FusionManager.OnFusionEnd.RemoveListener(FusionManager_OnFusionEnd);
    }

    private void FusionManager_OnFusionEnd(){
        Battle.ChangeState(Battle.CardStatSelection);
    }

    public override string ToString(){ return "Fusion"; }
}
