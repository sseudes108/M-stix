using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour {
    public static Action OnSomeCardSelected;
    public static Action OnNoneCardSelected;
    public static Action<List<Card>> OnSelectionFinished;

    [field:SerializeField] public List<Card> SelectedList { get; private set; }

    private void OnEnable() {
        Card.OnCardSelected += Card_OnCardSelected;
        Card.OnCardDeselected += Card_OnCardDeselected;
        UIBattleScene.OnSelectionFinished += UIBattleScene_OnSelectionFinished;
    }

    private void OnDisable() {
        Card.OnCardSelected -= Card_OnCardSelected;
        Card.OnCardDeselected -= Card_OnCardDeselected; 
        UIBattleScene.OnSelectionFinished -= UIBattleScene_OnSelectionFinished;
    }

    private void UIBattleScene_OnSelectionFinished(){
        OnSelectionFinished?.Invoke(SelectedList);
    }

    private void Card_OnCardSelected(Card selectedCard){
        if(SelectedList.Count == 0) { OnSomeCardSelected?.Invoke(); }
        AddToSelectedList(selectedCard);
    }

    private void Card_OnCardDeselected(Card selectedCard){
        RemoveFromSelectedList(selectedCard);
        if(SelectedList.Count == 0){ OnNoneCardSelected?.Invoke(); }
    }

    public void AddToSelectedList(Card selectedCard){
        SelectedList.Add(selectedCard);
    }
    
    public void RemoveFromSelectedList(Card selectedCard){
        SelectedList.Remove(selectedCard);
    }
}