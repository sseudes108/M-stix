using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class CardSelector : MonoBehaviour  {
    //     private readonly CardManagerSO _cardManager;
    //     // private readonly BattleManagerSO _battleManager;
        private List<Card> _selectedList = new();

    //     // public CardSelector(CardManagerSO cardManager, BattleManagerSO battleManager){
    //     //     _cardManager = cardManager;
    //     //     _battleManager = battleManager;
    //     //     SelectedList = new();
    //     // }

        public void AddToSelectedList(Card selectedCard){
            // if(SelectedList.Count == 0) { _cardManager.SomeCardSelected(); } //mostrar botao de finalizar seleção
            _selectedList.Add(selectedCard);
        }
        
        public void RemoveFromSelectedList(Card deselectedCard){
            _selectedList.Remove(deselectedCard);
            // if(SelectedList.Count == 0){ _cardManager.NoneCardSelected();} //ocultar botao de finalizar seleção
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