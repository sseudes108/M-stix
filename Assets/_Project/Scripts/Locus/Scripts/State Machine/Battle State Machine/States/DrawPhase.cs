public class DrawPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();
        DrawCards();
    }

    public override void Exit(){
        UnsubscribeEvents();
    }

    private void DrawCards(){
        if(Battle.TurnManager.CurrentTurn == 1){
            Battle.BattleManager.PlayerDraw();
            Battle.BattleManager.EnemyDraw();
        }else if(IsPlayerTurn){
            Battle.BattleManager.PlayerDraw();
        }else{
            Battle.BattleManager.EnemyDraw();
        }
    }

    public override void SubscribeEvents(){
        if(Battle.TurnManager.IsPlayerTurn()){
            Battle.PlayerHandManager.OnCardsDrew.AddListener(HandManager_OnCardsDrew);
        }else{
            Battle.EnemyHandManager.OnCardsDrew.AddListener(HandManager_OnCardsDrew);
        }
    }
    
    public override void UnsubscribeEvents(){
        if(Battle.TurnManager.IsPlayerTurn()){
            Battle.PlayerHandManager.OnCardsDrew.RemoveListener(HandManager_OnCardsDrew);
        }else{
            Battle.EnemyHandManager.OnCardsDrew.RemoveListener(HandManager_OnCardsDrew);
        }
    }

    private void HandManager_OnCardsDrew(){
        Battle.ChangeState(Battle.CardSelection);
    }

    public override string ToString(){ return "Draw"; }
}