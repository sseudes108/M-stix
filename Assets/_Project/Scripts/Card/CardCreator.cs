using UnityEngine;

namespace Mistix{
    public class CardCreator : MonoBehaviour{
        public static CardCreator Instance;
        [SerializeField] MonsterCard _monsterCardPrefab;
        [SerializeField] ArcaneCard _arcaneCardPrefab;

        private void Awake() {
            if(Instance != null){
                Errors.InstanceError(this);
            }
            Instance = this;
        }

        public Card CreateCard(ScriptableObject cardData, Deck _deckInUse){
            if(cardData is MonsterCardSO){
                MonsterCard newMonsterCard = _monsterCardPrefab;
                newMonsterCard.SetUpCardData(cardData);

                var newCard = Instantiate(newMonsterCard, TurnSystem.Instance.GetDeckPosition(), TurnSystem.Instance.GetDeckRotation());
                _deckInUse.RemoveCardFromDeck(cardData);

                return newCard;
            }else{;
                ArcaneCard newArcaneCard = _arcaneCardPrefab;
                newArcaneCard.SetUpCardData(cardData);

                var newCard = Instantiate(newArcaneCard, TurnSystem.Instance.GetDeckPosition(), TurnSystem.Instance.GetDeckRotation());
                _deckInUse.RemoveCardFromDeck(cardData);
                
                return newCard;
            }
        }
    }
}
