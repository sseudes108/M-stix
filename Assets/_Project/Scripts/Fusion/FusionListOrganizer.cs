using System.Collections.Generic;
using UnityEngine;

public class FusionListOrganizer : MonoBehaviour
{
    //private List<Card> _cardsInLine;
    public void CheckCardTypes(List<Card> cards){
        //_cardsInLine = cards;

        Debug.Log("CheckCardTypes");
        CardSO.CardType card1Type;
        CardSO.CardType card2Type;
        
        card1Type = cards[0].GetCardType();
        card2Type = cards[1].GetCardType();

        // Debug.Log(card1Type);
        // Debug.Log(card2Type); 

        if(card1Type == CardSO.CardType.Monster && card2Type == CardSO.CardType.Monster){
            List<MonsterCard> monstersToFusion = new(){
                cards[0].GetMonsterInfo(),
                cards[1].GetMonsterInfo()
            };
            Debug.Log("Coroutine Start");
            Fusion.Instance.StartMonsterFusion(monstersToFusion);
        }else{
            //Reinicar linha de fusão
            //Debug.Log("Arcane Card");
            RemoveCardFromList(cards, cards[0]);
            List<Card> newlist = cards;

            if(newlist.Count >= 2){
                CheckCardTypes(newlist);
            }
            Debug.Log("Line Ended");
            return;
        }
    }

    public void RemoveCardFromList(List<Card> cardsInLine, Card cardToRemove){
        cardToRemove.gameObject.SetActive(false);
        cardsInLine.Remove(cardToRemove);
    }
}
