using UnityEngine;

namespace Mistix{
    public class CardManager : MonoBehaviour {
        [SerializeField] private CardCreator _creator;
        [SerializeField] private CardSelector _selector;

        private void Awake() {
            _creator = GetComponent<CardCreator>();
        }

        public Card InstantiateCard(ScriptableObject cardData){
            return _creator.CreateCard(cardData);
        }
    }
}