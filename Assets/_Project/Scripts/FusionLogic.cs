using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionLogic : MonoBehaviour{
    public static FusionLogic Instance {get; private set;}
    [SerializeField] private Transform _fusionedCardPosition;

    [SerializeField] private CardBoardPlacer _cardBoardPlacer;
    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one FusionLogic instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;

        _cardBoardPlacer = GetComponent<CardBoardPlacer>();
    }

    public void Fusion(List<Card> selectedCards){
        StartCoroutine(FusionRoutine(selectedCards));
    }

    private IEnumerator FusionRoutine(List<Card> selectedCards){

        FusionCardsChecker.Instance.StartCheck(selectedCards);

        yield return new WaitForSeconds(3);

        Card fusionedCard = FusionCardsChecker.Instance.GetFusionedCard();

        fusionedCard.transform.SetPositionAndRotation(_fusionedCardPosition.transform.position, _fusionedCardPosition.transform.rotation);
        fusionedCard.transform.SetParent(_fusionedCardPosition);

        if(selectedCards.Count > 0){
            CardSelector.Instance.AddFusionedCardToTheSelectedList(fusionedCard);
        }else{
            _cardBoardPlacer.PlacePlayerMonsterCard(fusionedCard);
        }
    }
}
