using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector {
    public AICardSelector(AIActorSO actor){
        _actor = actor;
    }

    private AIActorSO _actor;

    public List<Card> _selectedList = new();
    public List<Card> SelectedList => _selectedList;

    public IEnumerator SelectCardRoutine(List<Card> cardsInHand){
        Debug.Log("Routine Started");
        SelectRandomCard(cardsInHand);
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        _actor.CardSelectionFinished();
        yield return null;
        Debug.Log("Routine Ended");
    }

    private void SelectRandomCard(List<Card> cardsInHand){
        Debug.Log($"{cardsInHand.Count}");
        var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
        AddToSelectedList(randomCard);
    }

    private void AddToSelectedList(Card card){
        _selectedList.Add(card);
    }
}