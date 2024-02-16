using UnityEngine;

namespace Mistix{
    public class CardCreator : MonoBehaviour{
        [SerializeField] MonsterCard _monsterCardPrefab;
        [SerializeField] ArcaneCard _arcaneCardPrefab;


        public Card CreateFusionedCard(ScriptableObject cardData, MonoBehaviour sender){
            CheckPositionToSpawnCard(sender, out Vector3 deckPosition, out Quaternion deckRotation);

            if (cardData is MonsterCardSO){
                MonsterCard newMonsterCard = _monsterCardPrefab;
                newMonsterCard.SetUpCardData(cardData);

                var newCard = Instantiate(newMonsterCard, deckPosition, deckRotation);
                return newCard;
            }
            else{
                ArcaneCard newArcaneCard = _arcaneCardPrefab;
                newArcaneCard.SetUpCardData(cardData);

                var newCard = Instantiate(newArcaneCard, deckPosition, deckRotation);
                return newCard;
            }
        }

        public Card CreateCard(ScriptableObject cardData, Deck _deckInUse, MonoBehaviour sender){

            CheckPositionToSpawnCard(sender, out Vector3 deckPosition, out Quaternion deckRotation);

            if (cardData is MonsterCardSO){
                MonsterCard newMonsterCard = _monsterCardPrefab;
                newMonsterCard.SetUpCardData(cardData);

                var newCard = Instantiate(newMonsterCard);
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
                deckPosition = BattleManager.Instance.CardSpawnLocations.GetPlayerDeckPosition();
                deckRotation = BattleManager.Instance.CardSpawnLocations.GetPlayerDeckRotation();
            }
            else if (sender is EnemyHand){
                deckPosition = BattleManager.Instance.CardSpawnLocations.GetEnemyDeckPosition();
                deckRotation = BattleManager.Instance.CardSpawnLocations.GetEnemyDeckRotation();
            }
            else{
                deckPosition = BattleManager.Instance.CardSpawnLocations.GetfusionCardSpawnerPosition();
                deckRotation = BattleManager.Instance.CardSpawnLocations.GetfusionCardSpawnerRotation();
            }
        }
    }
}
