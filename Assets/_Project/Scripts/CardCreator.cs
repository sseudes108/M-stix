using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour{

    [SerializeField] private ArcaneCard _arcanePrefab;
    [SerializeField] private MonsterCard _monsterPrefab;

    public Card CreateCard(CardSO cardData){      
        if(cardData.cardType == CardSO.CardType.Arcane){
            ArcaneCard newArcaneCard = _arcanePrefab;
            newArcaneCard.SetData(cardData);
            Card newCard = newArcaneCard.GetComponent<Card>();
            return newCard;
        }else{
            MonsterCard newMonsterCard = _monsterPrefab;
            newMonsterCard.SetData(cardData);
            Card newCard = newMonsterCard.GetComponent<Card>();
            return newCard;
        }
    }

    public void RemoveCreatedCardFromDeck(List<CardSO> deck, CardSO dataToRemove){
        deck.Remove(dataToRemove);
    }
}
