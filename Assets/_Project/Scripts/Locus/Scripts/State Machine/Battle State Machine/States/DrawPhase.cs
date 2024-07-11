public class DrawPhase : AbstractState{
    public override void Enter(){
        SubscribeEvents();
        DrawCards();
    }

    public override void Exit(){
        UnsubscribeEvents();
    }

    private void DrawCards(){
        if(CurrentTurn == 1){
            Battle.BattleManager.PlayerDraw();
            Battle.BattleManager.EnemyDraw();
        }else if(IsPlayerTurn){
            Battle.BattleManager.PlayerDraw();
        }else{
            Battle.BattleManager.EnemyDraw();
        }
    }

    public override void SubscribeEvents(){
        Battle.HandManager.OnCardsDrew.AddListener(HandManager_OnCardsDrew);
        // Hand.OnCardsDrew += Hand_OnCardsDrew;
    }
    
    public override void UnsubscribeEvents(){
        Battle.HandManager.OnCardsDrew.RemoveListener(HandManager_OnCardsDrew);
        // Hand.OnCardsDrew -= Hand_OnCardsDrew;
    }

    private void HandManager_OnCardsDrew(){
        Battle.ChangeState(Battle.CardSelection);
    }

    public override string ToString(){ return "Draw"; }
}