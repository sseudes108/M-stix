using System;
using System.Collections;
using System.Collections.Generic;

public class FusionPhase : AbstractState{
    // public static Action OnStartFusion;
    public static Action<List<Card>, bool> OnStartFusion;
    private List<Card> _selectedCards = new();

    public override void Enter(){
        SubscribeEvents();
        Battle.StartCoroutine(FusionPhaseRoutine());
    }

    public override void Exit(){
        UnsubscribeEvents();
    }


    public IEnumerator FusionPhaseRoutine(){
        yield return null;
        OnStartFusion?.Invoke(_selectedCards, IsPLayerTurn);

        // yield return null;
        // OnStartFusion?.Invoke(_selectedCards, IsPLayerTurn);
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
