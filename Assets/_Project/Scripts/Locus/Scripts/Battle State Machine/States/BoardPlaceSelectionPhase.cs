using System.Collections;

public class BoardPlaceSelectionPhase : AbstractState{   

    public override void Enter(){
        SubscribeEvents();
        Battle.BattleManager.BoardPlaceSelectionStart(Battle.FusionManager.ResultCard, Battle.TurnManager.IsPlayerTurn);

        if(!Battle.TurnManager.IsPlayerTurn){
            Battle.StartCoroutine(AIRoutine(Battle.FusionManager.ResultCard));
        }
    }

    public override void Exit(){
        UnsubscribeEvents();
    }

    public override void SubscribeEvents(){
        Battle.BoardManager.OnBoardPlaceSelected.AddListener(BoardManager_OnBoardPlaceSelected);
    }

    public override void UnsubscribeEvents(){
        Battle.BoardManager.OnBoardPlaceSelected.RemoveListener(BoardManager_OnBoardPlaceSelected);
    }

    private void BoardManager_OnBoardPlaceSelected(){
        Battle.BattleManager.BoardPlaceSelectionEnd(Battle.FusionManager.ResultCard, Battle.TurnManager.IsPlayerTurn);
        Battle.ChangeState(Battle.Action);
    }
    public IEnumerator AIRoutine(Card cardToPlace){
        yield return Battle.StartCoroutine(AI.Actor.BoardPlaceSelector.BoardSelectionRoutine(cardToPlace));
        yield return null;
    }

    public override string ToString(){
        return "Board Place Sel.";
    }
}