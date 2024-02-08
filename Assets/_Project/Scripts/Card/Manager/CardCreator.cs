using UnityEngine;

namespace Mistix{
    public class CardCreator : MonoBehaviour{
        [SerializeField] MonsterCard _monsterCardPrefab;
        [SerializeField] ArcaneCard _arcaneCardPrefab;


        public Card CreateFusionedCard(ScriptableObject cardData){
            if (cardData is MonsterCardSO){
                MonsterCard newMonsterCard = _monsterCardPrefab;
                newMonsterCard.SetUpCardData(cardData);

                var newCard = Instantiate(newMonsterCard);
                return newCard;
            }
            else{
                ArcaneCard newArcaneCard = _arcaneCardPrefab;
                newArcaneCard.SetUpCardData(cardData);

                var newCard = Instantiate(newArcaneCard);
                return newCard;
            }
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
                deckPosition = BattleManager.Instance.TurnSystem.GetPlayerDeckPosition();
                deckRotation = BattleManager.Instance.TurnSystem.GetPlayerDeckRotation();
            }
            else if (sender is EnemyHand){
                deckPosition = BattleManager.Instance.TurnSystem.GetEnemyDeckPosition();
                deckRotation = BattleManager.Instance.TurnSystem.GetEnemyDeckRotation();
            }
            else{
                deckPosition = BattleManager.Instance.TurnSystem.GetfusionCardSpawnerPosition();
                deckRotation = BattleManager.Instance.TurnSystem.GetfusionCardSpawnerRotation();
            }
        }
    }
}
