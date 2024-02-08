using System.Collections;
using System.Collections.Generic;
using Mistix;
using UnityEngine;

public class EnemyHand : Hand{
   
    protected override IEnumerator DrawCardRoutine(){
        do{
            var randomIndex = Random.Range(0, _deck.GetDeckInUse().Count);
            SetCardInHand(BattleManager.Instance.CardCreator.CreateCard(_deck.GetDeckInUse()[randomIndex], _deck, this));
            yield return new WaitForSeconds(0.5f);

        }while(_freePositionsInHand.Count > 0);
    }
} 