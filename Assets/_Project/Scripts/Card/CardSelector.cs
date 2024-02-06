using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class CardSelector:MonoBehaviour{
        public static CardSelector Instance;

        [SerializeField] private List<Card> _selectedCards;

        private void Awake() {
            if(Instance != null){
                Errors.InstanceError(this);
            }
            Instance = this;
        }

        public void AddCardToSelectedList(Card selectedCard){
            Debug.Log($"{selectedCard.name} Add to list");
            _selectedCards.Add(selectedCard);
        }
        
        public void RemoveCardFromSelectedList(Card deselectedCard){
            Debug.Log($"{deselectedCard.name} Removed from list");
            _selectedCards.Remove(deselectedCard);
        }

        public List<Card> GetSelectedCardList() => _selectedCards;
    }
}