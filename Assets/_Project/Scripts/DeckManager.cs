using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour{
    public List<CardSO> Deck => _deck;
    //Lists
    [SerializeField] private List<CardSO> _deck;
    
    public void RemovePickedCard(CardSO cardDataToRemove){
        _deck.Remove(cardDataToRemove);
    }

    public int CardsRemaining(){
        return _deck.Count;
    }
}
