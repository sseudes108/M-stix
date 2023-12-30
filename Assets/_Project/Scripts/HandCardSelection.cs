using System.Collections.Generic;
using UnityEngine;

public class HandCardSelection : MonoBehaviour{
    public List<Card> SelectedCards => _selectedCards;
    [SerializeField] private List<Card> _selectedCards;

    private void OnEnable() {
        Card.OnSelect += AddToSelecetedCards;
        Card.OnDiselect += RemoveFromSelectedCards;
        HandAction.OnFusionStarted += ClearSelectedCardList;
    }

    private void OnDisable() {
        Card.OnSelect -= AddToSelecetedCards;
        Card.OnDiselect -= RemoveFromSelectedCards;
        HandAction.OnFusionStarted -= ClearSelectedCardList;
    }

    private void ClearSelectedCardList(){
        Debug.Log("OnFusionStarted invoked");
        _selectedCards.Clear();
    }

    public void AddToSelecetedCards(Card sender){
        //sender.EnableCollider(false);
        _selectedCards.Add(sender);
    }

    public void RemoveFromSelectedCards(Card sender){
        //sender.EnableCollider(true);
        _selectedCards.Remove(sender);
    }
}
