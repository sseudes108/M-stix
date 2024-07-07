using System;

public class DrawPhase : AbstractState{
    public static Action OnPlayerDraw;
    public static Action OnEnemyDraw;

    public override void Enter(){
        SubscribeEvents();
        DrawCards();
    }

    public override void Exit(){
        UnsubscribeEvents();
    }

    private void DrawCards(){
        if(CurrentTurn == 1){
            OnPlayerDraw?.Invoke();
            OnEnemyDraw?.Invoke();

        }else if(IsPlayerTurn){
            OnPlayerDraw?.Invoke();
        }else{
            OnEnemyDraw?.Invoke();
        }
    }

    public override void SubscribeEvents(){
        Hand.OnCardsDrew += Hand_OnCardsDrew;
    }
    
    public override void UnsubscribeEvents(){
        Hand.OnCardsDrew -= Hand_OnCardsDrew;
    }

    private void Hand_OnCardsDrew(){
        Battle.ChangeState(Battle.CardSelection);
    }

    public override string ToString(){ return "Draw"; }
}