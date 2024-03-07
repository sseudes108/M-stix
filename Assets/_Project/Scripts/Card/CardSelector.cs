using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour {
    [SerializeField] private List<Card> _selectedCards;

    public void AddCardToSelectedList(Card selectedCard){
        _selectedCards.Add(selectedCard);
    }

    public void RemoveCardFromSelectedList(Card selectedCard){
        _selectedCards.Remove(selectedCard);
    }
    
    public List<Card> GetSelectedCards(){
        FreePlacesInHand();
        return _selectedCards;
    }

    private void FreePlacesInHand(){
        foreach(var card in _selectedCards){
            var handPosition = card.GetComponentInParent<HandPosition>();
            handPosition.SetHandPlaceFree();
        }
    }
}