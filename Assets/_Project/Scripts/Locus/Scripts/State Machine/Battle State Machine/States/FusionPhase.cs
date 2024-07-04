using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionPhase : AbstractState{
    public static Action<List<Card>, bool> OnStartFusion;
    private List<Card> _selectedCards = new();

    public override void Enter(){
        SubscribeEvents();
        if(Battle != null){
            Battle.StartCoroutine(FusionPhaseRoutine());
        }
    }

    public override void Exit(){
        UnsubscribeEvents();
    }
    
    public IEnumerator FusionPhaseRoutine(){
        yield return null;
        OnStartFusion?.Invoke(_selectedCards, IsPLayerTurn);
    }

    public override void SubscribeEvents(){
        CardSelector.OnSelectionFinished += CardSelector_OnSelectionFinished;
        Fusion.OnFusionEnd += Fusion_OnFusionEnd;
    }

    public override void UnsubscribeEvents(){
        CardSelector.OnSelectionFinished -= CardSelector_OnSelectionFinished;
        Fusion.OnFusionEnd += Fusion_OnFusionEnd;
    }

    private void Fusion_OnFusionEnd(Card card, bool arg2){
        Debug.Log("FusionPhase - Fusion_OnFusionEnd");
        Battle.ChangeState(Battle.CardStatSelection);
    }

    private void CardSelector_OnSelectionFinished(List<Card> list){
        _selectedCards = list;
    }

    public override string ToString(){ return "Fusion"; }
}
