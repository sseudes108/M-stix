using UnityEngine;

namespace Mistix{
    public class CardManager : MonoBehaviour {
        [SerializeField] private CardCreator _creator;
        [SerializeField] private CardSelector _selector;

        private void Awake() {
            _creator = GetComponent<CardCreator>();
            _selector = GetComponent<CardSelector>();
        }

        public Card InstantiateCard(ScriptableObject cardData){
            return _creator.CreateCard(cardData);
        }

        public void SelectCard(Card selectedCard){
            _selector.AddToSelectedList(selectedCard);
        }

        public void DeselectCard(Card deselectedCard){
            _selector.RemoveFromSelectedList(deselectedCard);
        }
    }
}