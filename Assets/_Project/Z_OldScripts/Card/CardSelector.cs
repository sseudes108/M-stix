// using System.Collections.Generic;
// using UnityEngine;

// public class CardSelector : MonoBehaviour {
//     [SerializeField] private List<Card> _selectedCards;
//     [SerializeField] private List<Card> _selectedPlayerCards;
//     [SerializeField] private List<Card> _selectedEnemyCards;

//     private void OnEnable() {
//         BPBoardPlaceSelection.OnBoardPlaceSelectionEnd += BattlePhaseBoardPlaceSelection_OnBoardSelectionEnd;
//     }

//     private void OnDisable() {
//         BPBoardPlaceSelection.OnBoardPlaceSelectionEnd += BattlePhaseBoardPlaceSelection_OnBoardSelectionEnd;
//     }

//     private void BattlePhaseBoardPlaceSelection_OnBoardSelectionEnd(){
//         ClearSelectedlist();
//     }

//     public void AddCardToSelectedList(Card selectedCard){
//         if(selectedCard.IsPlayerCard()){
//             _selectedCards = _selectedPlayerCards;
//         }else{
//             _selectedCards = _selectedEnemyCards;
//         }
//         _selectedCards.Add(selectedCard);
//     }

//     public void RemoveCardFromSelectedList(Card selectedCard){
//         if(selectedCard.IsPlayerCard()){
//             _selectedCards = _selectedPlayerCards;
//         }else{
//             _selectedCards = _selectedEnemyCards;
//         }
//         _selectedCards.Remove(selectedCard);
//     }
    
//     public List<Card> GetSelectedCards(){
//         FreePlacesOfSelectedCardsFromHand();
//         return _selectedCards;
//     }
//     public void ClearSelectedlist(){
//         _selectedCards.Clear();
//     }

//     private void FreePlacesOfSelectedCardsFromHand(){
//         foreach(var card in _selectedCards){
//             if(card != null){
//                 var handPosition = card.GetComponentInParent<HandPosition>();
//                 if(handPosition != null){
//                     handPosition.SetHandPlaceFree();
//                 }
//             }
//         }
//     }
// }