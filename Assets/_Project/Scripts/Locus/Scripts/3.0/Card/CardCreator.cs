using UnityEngine;

namespace Mistix{
    public class CardCreator : MonoBehaviour {

        [SerializeField] private MonsterCard _monsterCardPrefab;
        [SerializeField] private ArcaneCard _arcaneCardPrefab;

        public Card CreateCard(ScriptableObject cardData){
            Card newCard;
            if (cardData is MonsterCardSO){
                newCard = _monsterCardPrefab;
            }else{
                newCard = _arcaneCardPrefab;
            }
            newCard.SetCardData(cardData);

            return newCard;
        }
    }
}