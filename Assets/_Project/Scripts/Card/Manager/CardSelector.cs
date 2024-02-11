using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class CardSelector:MonoBehaviour{

        [SerializeField] private List<Card> _playerSelectedCards;
        [SerializeField] private List<Card> _enemySelectedCards;

        public void AddCardToSelectedList(Card cardToAdd){
            if(cardToAdd.IsPlayerCard()){
                // Debug.Log("Add to player selected list");
                AddCardToPlayerSelectedList(cardToAdd);
            }else{
                // Debug.Log("Add to enemy selected list");
                AddCardToEnemySelectedList(cardToAdd);
            }
        }

        public void RemoveCardFromSelectedList(Card cardToRemove){
            if(cardToRemove.IsPlayerCard()){
                // Debug.Log("Removed from player selected list");
                RemoveCardFromPlayerSelectedList(cardToRemove);
            }else{
                // Debug.Log("Removed from enemy selected list");
                RemoveCardFromEnemySelectedList(cardToRemove);
            }
        }

        public void AddResultCardToSelectedList(Card fusionedCard){
            if(fusionedCard.IsPlayerCard()){
                _playerSelectedCards.Insert(0, fusionedCard);
            }else{
                _enemySelectedCards.Insert(0, fusionedCard);
            }
        }

        //Player
        private void AddCardToPlayerSelectedList(Card selectedCard){
            _playerSelectedCards.Add(selectedCard);
        }
        
        private void RemoveCardFromPlayerSelectedList(Card deselectedCard){
            _playerSelectedCards.Remove(deselectedCard);
        }
        
        //Enemy
        private void AddCardToEnemySelectedList(Card selectedCard){
            _enemySelectedCards.Add(selectedCard);
        }
        
        private void RemoveCardFromEnemySelectedList(Card deselectedCard){
            _enemySelectedCards.Remove(deselectedCard);
        }

        public List<Card> GetSelectedPlayerCardList() => _playerSelectedCards;
        public List<Card> GetSelectedEnemyCardList() => _enemySelectedCards;
    }
}