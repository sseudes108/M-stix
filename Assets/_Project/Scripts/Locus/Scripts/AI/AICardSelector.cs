using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector {
    private AIActorSO _actor;
    public AICardSelector(AIActorSO actor){
        _actor = actor;
    }

    public List<Card> _selectedList = new();
    public List<Card> SelectedList => _selectedList;

    public IEnumerator SelectCardRoutine(List<Card> cardsInHand){
        SelectRandomCard(cardsInHand);
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        _actor.CardSelectionFinished();
        yield return null;
    }

    public void SelectRandomCard(List<Card> cardsInHand){
        Debug.Log($"{cardsInHand.Count}");
        var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
        AddToSelectedList(randomCard);
    }

    public void AddToSelectedList(Card card){
        _selectedList.Add(card);
    }
}