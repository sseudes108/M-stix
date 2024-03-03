using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour {
    List<Card> _selectedCards = new();

    public void AddCardToSelectedList(Card selectedCard){
        _selectedCards.Add(selectedCard);
    }

    public void RemoveCardFromSelectedList(Card selectedCard){
        _selectedCards.Remove(selectedCard);
    }
    
    public List<Card> GetSelectedCards() => _selectedCards;
}