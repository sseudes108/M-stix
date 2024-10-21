public class DrawPhase : AbstractState{
    public DrawPhase(StateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){
        SubscribeEvents();
        DrawCards();
    }

    public override void Exit() { UnsubscribeEvents(); }

    private void DrawCards(){
        if(StateMachine.Battle.TurnManager.CurrentTurn == 1){
            StateMachine.Battle.BattleManager.PlayerDraw();
            StateMachine.Battle.Board.PlayerHand.CheckPositionsInHand();

            StateMachine.Battle.BattleManager.EnemyDraw();
            StateMachine.Battle.Board.EnemyHand.CheckPositionsInHand();

            return;
        }

        if(StateMachine.Battle.TurnManager.IsPlayerTurn){

            StateMachine.Battle.BattleManager.PlayerDraw();
            StateMachine.Battle.Board.PlayerHand.CheckPositionsInHand();
            
            return;
        }
        StateMachine.Battle.BattleManager.EnemyDraw();
        StateMachine.Battle.Board.EnemyHand.CheckPositionsInHand();
    }

    public override void SubscribeEvents(){
        if(StateMachine.Battle.TurnManager.IsPlayerTurn){
            StateMachine.Battle.PlayerHandManager.OnCardsDrew.AddListener(HandManager_OnCardsDrew);
            return;
        }
        
        StateMachine.Battle.EnemyHandManager.OnCardsDrew.AddListener(HandManager_OnCardsDrew);
    }
    
    public override void UnsubscribeEvents(){
        if(StateMachine.Battle.TurnManager.IsPlayerTurn){
            StateMachine.Battle.PlayerHandManager.OnCardsDrew.RemoveListener(HandManager_OnCardsDrew);
            return;
        }

        StateMachine.Battle.EnemyHandManager.OnCardsDrew.RemoveListener(HandManager_OnCardsDrew);
    }

    private void HandManager_OnCardsDrew() { StateMachine.Battle.ChangeState(StateMachine.Battle.CardSelection); }

    public override string ToString() { return "Draw"; }
}