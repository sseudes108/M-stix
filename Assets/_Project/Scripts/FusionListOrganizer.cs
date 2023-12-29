using System.Collections.Generic;
using UnityEngine;

public class FusionListOrganizer : MonoBehaviour
{
    public void CheckCardTypes(List<Card> cards){
        Debug.Log("CheckCardTypes");
        CardSO.CardType card1Type;
        CardSO.CardType card2Type;
        
        card1Type = cards[0].GetCardType();
        card2Type = cards[1].GetCardType();

        Debug.Log(card1Type);
        Debug.Log(card2Type);

        if(card1Type == CardSO.CardType.Monster && card2Type == CardSO.CardType.Monster){
            List<MonsterCard> monstersToFusion = new(){
                cards[0].GetMonsterInfo(),
                cards[1].GetMonsterInfo()
            };
            Debug.Log("Coroutine Start");
            Fusion.Instance.StartMonsterFusion(monstersToFusion);
        }else{
            Debug.Log("Arcane Card");
            cards[0].gameObject.SetActive(false);
        }
    }
}
