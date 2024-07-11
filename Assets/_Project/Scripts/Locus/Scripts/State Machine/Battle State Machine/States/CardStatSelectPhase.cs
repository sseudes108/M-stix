using System;


public class CardStatSelectPhase : AbstractState{
    // public static Action<Card> OnStatSelectStart;
    // public static Action<Card, bool>  OnStatSelectEnd;

    public override void Enter(){
        SubscribeEvents();
        GameManager.Instance.UI.CardStats.FusionEnded(ResultCard);
        Battle.BattleManager.StatSelectStart(ResultCard);
        // OnStatSelectStart?.Invoke(ResultCard);
    }

    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.StatSelectEnd(ResultCard, IsPlayerTurn);
        // OnStatSelectEnd?.Invoke(ResultCard, IsPlayerTurn);
    }

    public override void SubscribeEvents(){
        CardStatSelections.OnSelectionsEnd += StatSelections_OnSelectionEnd;
    }

    public override void UnsubscribeEvents(){
        CardStatSelections.OnSelectionsEnd -= StatSelections_OnSelectionEnd;
    }

    private void StatSelections_OnSelectionEnd(){
        Battle.ChangeState(Battle.BoardPlaceSelection);
    }

    public override string ToString(){
        return "Card Stats Sel.";
    }
}