using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionLogic : MonoBehaviour{
    public static FusionLogic Instance {get; private set;}
    [SerializeField] private Transform _fusionedCardPosition;
    [SerializeField] private CardBoardPlacer _cardBoardPlacer;
    public Action OnFusionStarted, OnFusionEnded;

    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one FusionLogic instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;

        _cardBoardPlacer = GetComponent<CardBoardPlacer>();
    }

    public void StartFusion(){
        List<Card> selectedCards = CardSelector.Instance.SelectedCards;
        if(selectedCards.Count <= 1) return;
        OnFusionStarted?.Invoke();

        StartCoroutine(FusionRoutine());
    }

    private IEnumerator FusionRoutine(){
        List<Card> selectedCards = CardSelector.Instance.SelectedCards;

        FusionCardsChecker.Instance.StartCheck(selectedCards);
        yield return new WaitForSeconds(1f);

        Card resultCard = Instantiate(FusionCardsChecker.Instance.GetResultCard());
        yield return new WaitForSeconds(0.5f);

        if(selectedCards.Count != 0){
            resultCard.transform.SetPositionAndRotation(_fusionedCardPosition.position, _fusionedCardPosition.rotation);
            resultCard.transform.SetParent(_fusionedCardPosition);
            CardSelector.Instance.AddFusionedCardToTheSelectedList(resultCard);

            StartFusion();

        }else{
            if(resultCard.GetCardType() == Card.CardType.Monster){
                _cardBoardPlacer.PlacePlayerMonsterCard(resultCard);

            }else{
                _cardBoardPlacer.PlacePlayerArcaneCard(resultCard);
            }
        }
        OnFusionEnded?.Invoke();
    }
}
