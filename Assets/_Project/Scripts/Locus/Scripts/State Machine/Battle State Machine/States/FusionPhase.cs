using System;
using System.Collections;
using System.Collections.Generic;

public class FusionPhase : AbstractState{
    public static Action<List<Card>, bool> OnMoveCardsToPosition;
    private List<Card> _selectedCards = new();

    public override void Enter(){
        SubscribeEvents();
        Battle.StartCoroutine(FusionPhaseRoutine());
    }

    public override void Exit(){
        UnsubscribeEvents();
    }

    public override void LogicUpdate(){}

    public IEnumerator FusionPhaseRoutine(){
        yield return null;
        OnMoveCardsToPosition?.Invoke(_selectedCards, IsPLayerTurn);
    }

    public override void SubscribeEvents(){
        CardSelector.OnSelectionFinished += CardSelector_OnSelectionFinished;
    }

    public override void UnsubscribeEvents(){
        CardSelector.OnSelectionFinished -= CardSelector_OnSelectionFinished;
    }

    private void CardSelector_OnSelectionFinished(List<Card> list){
        _selectedCards = list;
    }

    public override string ToString(){ return "Fusion"; }
}
