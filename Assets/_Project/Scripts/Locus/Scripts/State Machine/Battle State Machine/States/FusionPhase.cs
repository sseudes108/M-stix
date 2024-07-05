using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionPhase : AbstractState{
    // public static Action<List<Card>, bool> OnStartFusion;
    private List<Card> _selectedCards = new();

    public override void Enter(){
        SubscribeEvents();
        // Battle.StartCoroutine(FusionPhaseRoutine());
    }

    public override void Exit(){
        UnsubscribeEvents();
    }
    
    public IEnumerator FusionPhaseRoutine(){
        Debug.Log("FusionPhaseRoutine");
        GameManager.Instance.Fusion.Fusion.SetFusionSettings(_selectedCards, IsPLayerTurn);
        GameManager.Instance.Fusion.Positions.MoveCardsToFusionPosition(_selectedCards, IsPLayerTurn);
        // OnStartFusion?.Invoke(_selectedCards, IsPLayerTurn);
        yield return null;
        GameManager.Instance.Fusion.Fusion.StartFusionRoutine();
    }

    public override void SubscribeEvents(){
        Debug.Log("FusionPhase - SubscribeEvents");
        CardSelector.OnSelectionFinished += CardSelector_OnSelectionFinished;
        // Fusion.OnFusionEnd += Fusion_OnFusionEnd;
        Fusion.OnFusionEnd += StartFusionEnd;
    }

    public override void UnsubscribeEvents(){
        Debug.Log("FusionPhase - UnsubscribeEvents");
        CardSelector.OnSelectionFinished -= CardSelector_OnSelectionFinished;
        // Fusion.OnFusionEnd += Fusion_OnFusionEnd;
        Fusion.OnFusionEnd += StartFusionEnd;
    }

    public void StartFusionEnd(){
        Battle.StartCoroutine(FusionEnd());
    }

    public IEnumerator FusionEnd(){
        yield return null;
        Battle.ChangeState(Battle.CardStatSelection);
    }

    private void Fusion_OnFusionEnd(){
        Debug.Log("FusionPhase - Fusion_OnFusionEnd");
        Battle.ChangeState(Battle.CardStatSelection);
    }

    private void CardSelector_OnSelectionFinished(List<Card> list){
        Debug.Log("FusionPhase - CardSelector_OnSelectionFinished");
        _selectedCards = list;
        if(Battle != null){
            Battle.StartCoroutine(FusionPhaseRoutine());
        }
    }

    public override string ToString(){ return "Fusion"; }
}
