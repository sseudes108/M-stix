using System.Collections.Generic;
using UnityEngine;

public class HandEnemy : Hand {
    [SerializeField] private List<Card> cardsInEnemyHand;
    protected override void SetHand(){
        _hand = GetComponent<HandPlayer>();
    }
    protected override void SetDeck(){
        _deck = GetComponentInChildren<Deck>();
    }

    protected override void EndDrawPhase(){
        //Solve the stack overflow excepction (Two hands trying to end the draw phase at the sime time)
        if(BattleManager.Instance.TurnManager.GetTurn() != 0 && !BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.CardSelectionPhase);
        }
    }
    
    public List<Card> GetCardsInHand(){
        cardsInEnemyHand = new();
        foreach(var position in _handPositions){
            var card = position.GetComponentInChildren<Card>();
            cardsInEnemyHand.Add(card);
        }
        return cardsInEnemyHand;
    }
}