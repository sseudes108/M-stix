using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class CardSelector : MonoBehaviour  {
        private List<Card> _selectedList = new();
        private CardManager _cardManager;

        private void Awake() {
            _cardManager = FindFirstObjectByType<CardManager>();
        }

        public void AddToSelectedList(Card selectedCard){
            if(_selectedList.Count == 0) { _cardManager.ShowEndSelectionButton(); }
            _selectedList.Add(selectedCard);
        }
        
        public void RemoveFromSelectedList(Card deselectedCard){
            _selectedList.Remove(deselectedCard);
            if(_selectedList.Count == 0) { _cardManager.HideEndSelectionButton(); }
        }

        public void RemoveSelectedCardsInHand(){
            foreach(var card in _selectedList){
                card.SetCardOnHand(false);
            }
        }

        public List<Card> GetSelectedCards(){ return _selectedList; }
    }
}