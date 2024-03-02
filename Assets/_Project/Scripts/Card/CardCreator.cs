using UnityEngine;

public class CardCreator : MonoBehaviour {
    [SerializeField] private CardMonster _monsterCardPrefab;
    [SerializeField] private CardArcane _arcaneCardPrefab;

    public Card CreateCard(ScriptableObject cardData){
        Card newCard;

        if(cardData is CardMonsterSO){
            newCard = _monsterCardPrefab;
            
        }else{
            newCard = _arcaneCardPrefab;
        }

        newCard.SetCardData(cardData);
        
        return newCard;
    }
}