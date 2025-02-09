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

    //     // public void RegisterEvents(){
    //     //     _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    //     // }
        
    //     // public void UnregisterEvents(){
    //     //     _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    //     // }

    //     private void BattleManager_OnStartPhase(){
    //     SelectedList.Clear();
    //     }

    //     public void SetCardsToBoardFusion(List<Card> selectedCards){
    //         SelectedList.Clear();
    //         SelectedList = selectedCards;
    //     }
    // }
    }
}