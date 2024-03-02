using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour {
    [SerializeField] List<Card> _selectedCards;

    public void AddCardToSelectedList(Card selectedCard){
        _selectedCards.Add(selectedCard);
    }

    public void RemoveCardFromSelectedList(Card selectedCard){
        _selectedCards.Remove(selectedCard);
    }

    public List<Card> GetSelectedCards() => _selectedCards;
}