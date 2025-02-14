using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class CardManager : MonoBehaviour {
        private CardCreator _creator;
        private CardSelector _selector;
        private CardStatsSelecion _statsSelecion;

        [SerializeField] private BattleManager _battleManager;

        private bool _allStatsSelected;

        private void Awake() {
            _creator = GetComponent<CardCreator>();
            _selector = GetComponent<CardSelector>();
            _statsSelecion = GetComponent<CardStatsSelecion>();
        }

        public Card DrawCard(ScriptableObject cardData){ return _creator.CreateCard(cardData); }

        public void SelectCard(Card selectedCard){ _selector.AddToSelectedList(selectedCard); }

        public void DeselectCard(Card deselectedCard){ _selector.RemoveFromSelectedList(deselectedCard); }

        public void ShowEndSelectionButton(){ _battleManager.ShowEndSelectionButton(); }

        public void HideEndSelectionButton(){ _battleManager.HideEndSelectionButton(); }

        public void UpdateCardUilustration(Texture2D illustration){ _battleManager.UpdateCardUilustration(illustration); }

        public List<Card> GetSelectedCards(){ return _selector.GetSelectedCards(); }

        public void SelectAnother(MonsterCard monster){
            _battleManager.SelectAnother(monster);
        }

        public void StatSelectionEnd(){
            _allStatsSelected = true;
            _battleManager.StatSelectionEnd();
        }

        public void Option1_Clicked(Card card){
            _statsSelecion.Option1_Clicked(card);
        }

        public void Option2_Clicked(Card card){
            _statsSelecion.Option2_Clicked(card);
        }

        public bool IsAllStatsSelected(){ return _allStatsSelected; }
        public void SetAllStatsSelectedFalse() { _allStatsSelected = false; }
    }
}