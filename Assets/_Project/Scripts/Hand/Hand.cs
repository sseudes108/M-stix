using System.Collections;
using System.Collections.Generic;
using Mistix;
using UnityEngine;

public class Hand : MonoBehaviour{
    [SerializeField] List<HandPosition> _handPlaces;
    [SerializeField] private List<Transform> _freePositionsInHand;
    private Deck _deck;

    private void Awake() {
        _deck = GetComponentInParent<Deck>();
        _freePositionsInHand = new();
    }

    private void Start() {
        VerifyPositionsInHand();
    }
    public void StartDrawCardRoutine(){
        StartCoroutine(DrawCardRoutine());
    }

    public IEnumerator DrawCardRoutine(){
        do{
            var randomIndex = Random.Range(0, _deck.GetDeckInUse().Count);

            InstantiateCard(CardCreator.Instance.CreateCard(_deck.GetDeckInUse()[randomIndex]));
            yield return new WaitForSeconds(0.5f);

        }while(_freePositionsInHand.Count > 0);
    }

    private void InstantiateCard(Card cardDrew){
        var newCardDrew = Instantiate(cardDrew, _freePositionsInHand[0].transform.position, _freePositionsInHand[0].transform.rotation);
        newCardDrew.gameObject.transform.SetParent(_freePositionsInHand[0]);
        
        newCardDrew.name = $"{newCardDrew.GetCardInfo()}";
        
        _freePositionsInHand[0].GetComponent<HandPosition>().OcupyPlace();
        VerifyPositionsInHand();
    }

    public void VerifyPositionsInHand(){
        _freePositionsInHand.Clear();
        foreach(var position in _handPlaces){
            if(position.IsFree()){
                var positionTransform = position.gameObject.transform;
                _freePositionsInHand.Add(positionTransform);
            }
        }
    }
}