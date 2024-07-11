using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour {
    [SerializeField] private CardEventHandlerSO CardManager;

    [field:SerializeField] public List<Card> SelectedList { get; private set; }

    public void AddToSelectedList(Card selectedCard){
        if(SelectedList.Count == 0) { CardManager.SomeCardSelected(); }
        SelectedList.Add(selectedCard);
    }
    
    public void RemoveFromSelectedList(Card selectedCard){
        SelectedList.Remove(selectedCard);
        if(SelectedList.Count == 0){ CardManager.NoneCardSelected();}
    }
}