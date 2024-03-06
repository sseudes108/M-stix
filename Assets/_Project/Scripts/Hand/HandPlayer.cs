using UnityEngine;

public class HandPlayer : Hand {
    protected override void SetHand(){
        _hand = GetComponent<HandPlayer>();
    }
    protected override void SetDeck(){
        _deck = GetComponentInChildren<Deck>();
    }

    protected override void EndDrawPhase(){
        //Solve the stack overflow excepction (Two hands trying to end the draw phase at the sime time)
        if(BattleManager.Instance.TurnManager.GetTurn() == 0 || BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.SelectionPhase);
        }
    }
}