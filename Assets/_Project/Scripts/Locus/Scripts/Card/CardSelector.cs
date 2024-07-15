using System.Collections.Generic;

public class CardSelector {
    public CardManagerSO _cardManager;
    public List<Card> SelectedList { get; private set; }

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

    public void ClearList(){
        SelectedList.Clear();
    }
}