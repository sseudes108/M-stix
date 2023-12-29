// using System.Collections.Generic;
// using UnityEngine;

// public class CardSelector : MonoBehaviour{
//     public List<Card> SelectedCards => _selectedCards;
//     public static CardSelector Instance;
//     [SerializeField] private List<Card> _selectedCards;

//     private void OnEnable() {
//         FusionManager.OnFusion += ResetSelectedCards;
//     }
//     private void OnDisable() {
//         FusionManager.OnFusion -= ResetSelectedCards;
//     }
//     private void Awake() {
//         if(Instance == null){Instance = this;}
//     }

//     public void AddSelectedCard(Card card, string name){
//         _selectedCards.Add(card);
//     }

//     private void ResetSelectedCards(){
//         _selectedCards.Clear();
//     }
// }
