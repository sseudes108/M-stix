using System.Collections;

public class CardStatSelectPhase : AbstractState{

    public override void Enter(){
        SubscribeEvents();
        Battle.BattleManager.StatSelectStart(Battle.FusionManager.ResultCard);

        if(!Battle.BattleManager.IsPlayerTurn){
            Battle.StartCoroutine(AIRoutine(Battle.FusionManager.ResultCard));
        }
    }

    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.StatSelectEnd(Battle.FusionManager.ResultCard, Battle.BattleManager.IsPlayerTurn);
    }

    public override void SubscribeEvents(){
        if(Battle.BattleManager.IsPlayerTurn){
            Battle.CardStatSelManager.OnSelectionsEnd.AddListener(CardStatSelManager_OnSelectionsEnd);
            return;
        }

        AI.Actor.CardStatSelector_OnCardStatSelectionFinished.AddListener(AI_Actor_CardStatSelectior_OnCardStatSelectionFinished);
    }

    public override void UnsubscribeEvents(){
        if(Battle.BattleManager.IsPlayerTurn){
            Battle.CardStatSelManager.OnSelectionsEnd.RemoveListener(CardStatSelManager_OnSelectionsEnd);
            return;
        }

        AI.Actor.CardStatSelector_OnCardStatSelectionFinished.AddListener(AI_Actor_CardStatSelectior_OnCardStatSelectionFinished);
    }

    private void CardStatSelManager_OnSelectionsEnd() { ChangePhase(); }
    private void AI_Actor_CardStatSelectior_OnCardStatSelectionFinished() { ChangePhase(); }
    private void ChangePhase() { Battle.ChangeState(Battle.BoardPlaceSelection); }

    public IEnumerator AIRoutine(Card resultCard){
        yield return Battle.StartCoroutine(AI.Actor.CardStatSelector.SelectCardStats(resultCard));
        yield return null;
    }

    public override string ToString() { return "Card Stats Sel."; }
}