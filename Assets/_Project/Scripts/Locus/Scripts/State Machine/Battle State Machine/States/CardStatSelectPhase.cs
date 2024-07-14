
public class CardStatSelectPhase : AbstractState{

    public override void Enter(){
        SubscribeEvents();
        // Battle.BattleManager.StatSelectStart(ResultCard);
        Battle.BattleManager.StatSelectStart(Battle.FusionManager.ResultCard);
    }

    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.StatSelectEnd(Battle.FusionManager.ResultCard, Battle.BattleManager.IsPlayerTurn);
        // Battle.BattleManager.StatSelectEnd(ResultCard, IsPlayerTurn);
    }

    public override void SubscribeEvents(){
        if(Battle.BattleManager.IsPlayerTurn){
            Battle.CardStatSelManager.OnSelectionsEnd.AddListener(CardStatSelManager_OnSelectionsEnd);
        }
    }

    public override void UnsubscribeEvents(){
        if(Battle.BattleManager.IsPlayerTurn){
            Battle.CardStatSelManager.OnSelectionsEnd.RemoveListener(CardStatSelManager_OnSelectionsEnd);
        }
    }

    private void CardStatSelManager_OnSelectionsEnd(){
        Battle.ChangeState(Battle.BoardPlaceSelection);
    }

    public override string ToString(){
        return "Card Stats Sel.";
    }
}