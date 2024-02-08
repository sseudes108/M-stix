using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class CardSelector:MonoBehaviour{

        [SerializeField] private List<Card> _playerSelectedCards;
        [SerializeField] private List<Card> _enemySelectedCards;

        //Player
        public void AddCardToPlayerSelectedList(Card selectedCard){
            Debug.Log($"{selectedCard.name} Add to list");
            _playerSelectedCards.Add(selectedCard);
        }
        
        public void RemoveCardFromPlayerSelectedList(Card deselectedCard){
            Debug.Log($"{deselectedCard.name} Removed from list");
            _playerSelectedCards.Remove(deselectedCard);
        }
        public List<Card> GetSelectedPlayerCardList() => _playerSelectedCards;

        //Enemy
        public void AddCardToEnemySelectedList(Card selectedCard){
            Debug.Log($"{selectedCard.name} Add to list");
            _enemySelectedCards.Add(selectedCard);
        }
        
        public void RemoveCardFromEnemySelectedList(Card deselectedCard){
            Debug.Log($"{deselectedCard.name} Removed from list");
            _enemySelectedCards.Remove(deselectedCard);
        }
        public List<Card> GetSelectedEnemyCardList() => _enemySelectedCards;
    }
}