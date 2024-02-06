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

        public Card CreateCard(ScriptableObject cardData, Deck _deckInUse, MonoBehaviour sender){

            CheckPositionToSpawnCard(sender, out Vector3 deckPosition, out Quaternion deckRotation);

            if (cardData is MonsterCardSO){
                MonsterCard newMonsterCard = _monsterCardPrefab;
                newMonsterCard.SetUpCardData(cardData);

                var newCard = Instantiate(newMonsterCard, deckPosition, deckRotation);
                _deckInUse.RemoveCardFromDeck(cardData);

                return newCard;
            }
            else{
                ArcaneCard newArcaneCard = _arcaneCardPrefab;
                newArcaneCard.SetUpCardData(cardData);

                var newCard = Instantiate(newArcaneCard, deckPosition, deckRotation);
                _deckInUse.RemoveCardFromDeck(cardData);

                return newCard;
            }
        }

        private static void CheckPositionToSpawnCard(MonoBehaviour sender, out Vector3 deckPosition, out Quaternion deckRotation){

            if (sender is PlayerHand){
                deckPosition = TurnSystem.Instance.GetPlayerDeckPosition();
                deckRotation = TurnSystem.Instance.GetPlayerDeckRotation();
            }
            else if (sender is EnemyHand){
                deckPosition = TurnSystem.Instance.GetEnemyDeckPosition();
                deckRotation = TurnSystem.Instance.GetEnemyDeckRotation();
            }
            else{
                deckPosition = TurnSystem.Instance.GetfusionCardSpawnerPosition();
                deckRotation = TurnSystem.Instance.GetfusionCardSpawnerRotation();
            }
        }
    }
}
