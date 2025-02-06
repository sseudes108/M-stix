// using System.Collections.Generic;

// public class CardSelector {
//     private readonly CardManagerSO _cardManager;
//     private readonly BattleManagerSO _battleManager;
//     public List<Card> SelectedList { get; private set; }

//     public CardSelector(CardManagerSO cardManager, BattleManagerSO battleManager){
//         _cardManager = cardManager;
//         _battleManager = battleManager;
//         SelectedList = new();
//     }

//     public void AddToSelectedList(Card selectedCard){
//         if(SelectedList.Count == 0) { _cardManager.SomeCardSelected(); }
//         SelectedList.Add(selectedCard);
//     }
    
//     public void RemoveFromSelectedList(Card selectedCard){
//         SelectedList.Remove(selectedCard);
//         if(SelectedList.Count == 0){ _cardManager.NoneCardSelected();}
//     }

//     public void RegisterEvents(){
//         _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
//     }
    
//     public void UnregisterEvents(){
//         _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
//     }

//     private void BattleManager_OnStartPhase(){
//        SelectedList.Clear();
//     }

//     public void SetCardsToBoardFusion(List<Card> selectedCards){
//         SelectedList.Clear();
//         SelectedList = selectedCards;
//     }
// }