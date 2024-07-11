using System.Collections;
using System.Collections.Generic;

public class FusionPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();
        var selectedCard = GameManager.Instance.CardManager.Selector.SelectedList;
        if(Battle!= null){
            Battle.StartCoroutine(FusionPhaseRoutine(selectedCard));
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
        // Battle.FusionManager.FusionEnd();
        Battle.FusionManager.OnFusionEnd.AddListener(FusionManager_OnFusionEnd);
        // Fusion.OnFusionEnd += Fusion_OnFusionEnd;
    }

    public override void UnsubscribeEvents(){
        Battle.FusionManager.OnFusionEnd.RemoveListener(FusionManager_OnFusionEnd);
        // Fusion.OnFusionEnd -= Fusion_OnFusionEnd;
    }

    private void FusionManager_OnFusionEnd(){
        Battle.ChangeState(Battle.CardStatSelection);
    }

    public override string ToString(){ return "Fusion"; }
}
