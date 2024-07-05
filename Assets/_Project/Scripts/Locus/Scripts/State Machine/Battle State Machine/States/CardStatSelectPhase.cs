using System;

public class CardStatSelectPhase : AbstractState{
    public static Action<Card> OnStatSelectStart;
    public static Action<Card, bool>  OnStatSelectEnd;

    public override void Enter(){
        SubscribeEvents();
        OnStatSelectStart?.Invoke(ResultCard);
    }

    public override void Exit(){
        UnsubscribeEvents();
        OnStatSelectEnd?.Invoke(ResultCard, IsPLayerTurn);
    }

    public override void SubscribeEvents(){
        StatSelections.OnSelectionsEnd += StatSelections_OnSelectionEnd;
    }

    public override void UnsubscribeEvents(){
        StatSelections.OnSelectionsEnd -= StatSelections_OnSelectionEnd;
    }

    private void StatSelections_OnSelectionEnd(){
        Battle.ChangeState(Battle.BoardPlaceSelection);
    }

    public override string ToString(){
        return "Card Stats Sel.";
    }
}