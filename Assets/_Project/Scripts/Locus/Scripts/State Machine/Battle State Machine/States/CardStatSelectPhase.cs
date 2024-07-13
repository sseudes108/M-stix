using UnityEngine;

public class CardStatSelectPhase : AbstractState{

    public override void Enter(){
        SubscribeEvents();
        Debug.Log("CardStatSelectPhase - Enter() <color=red>1</color=red> "); 
        GameManager.Instance.UI.CardStats.FusionEnded(ResultCard); //**** Result Card Nao definido ****//
        Battle.BattleManager.StatSelectStart(ResultCard);
    }

    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.StatSelectEnd(ResultCard, IsPlayerTurn);
    }

    public override void SubscribeEvents(){
        Battle.CardStatSelManager.OnSelectionsEnd.AddListener(CardStatSelManager_OnSelectionsEnd);
    }

    public override void UnsubscribeEvents(){
        Battle.CardStatSelManager.OnSelectionsEnd.RemoveListener(CardStatSelManager_OnSelectionsEnd);
    }

    private void CardStatSelManager_OnSelectionsEnd(){
        Battle.ChangeState(Battle.BoardPlaceSelection);
    }

    public override string ToString(){
        return "Card Stats Sel.";
    }
}