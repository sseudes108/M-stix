using System.Collections.Generic;
using UnityEngine;

public class CardSelector {
    private CardManagerSO _cardManager;
    [field:SerializeField] public List<Card> SelectedList { get; private set; }

    public CardSelector(CardManagerSO cardManager){
        _cardManager = cardManager;
        SelectedList = new();
    }

    public void AddToSelectedList(Card selectedCard){
        if(SelectedList.Count == 0) { _cardManager.SomeCardSelected(); }
        SelectedList.Add(selectedCard);
    }
    
    public void RemoveFromSelectedList(Card selectedCard){
        SelectedList.Remove(selectedCard);
        if(SelectedList.Count == 0){ _cardManager.NoneCardSelected();}
    }
}