using System.Collections;
using UnityEngine;

public class AICardSelector : MonoBehaviour {

    public void StartCardSelection(){
        StartCoroutine(SelectCardsInEnemyHand());
    }

    private IEnumerator SelectCardsInEnemyHand(){
        var cardsInHand = BattleManager.Instance.EnemyHand.GetCardsInHand();

        //Send all cards to fusion
        // foreach(var card in cardsInHand){
        //     BattleManager.Instance.CardSelector.AddCardToSelectedList(card);
        // }

        //Select one random card
        BattleManager.Instance.CardSelector.AddCardToSelectedList(cardsInHand[Random.Range(0,5)]);

        yield return new WaitForSeconds(2f);
        // Debug.Log("Starting Fusion");
        yield return new WaitForSeconds(1f);

        BattleManager.Instance.BattleStateManager.BattlePhaseCardSelection.EndSelection();
    }
}