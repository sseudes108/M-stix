using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour {
    public static Action OnSomeCardSelected;
    public static Action OnNoneCardSelected;

    [field:SerializeField] public List<Card> SelectedList { get; private set; }

    public void AddToSelectedList(Card selectedCard){
        if(SelectedList.Count == 0) { OnSomeCardSelected?.Invoke(); }
        SelectedList.Add(selectedCard);
    }
    
    public void RemoveFromSelectedList(Card selectedCard){
        SelectedList.Remove(selectedCard);
        if(SelectedList.Count == 0){ OnNoneCardSelected?.Invoke(); }
    }
}