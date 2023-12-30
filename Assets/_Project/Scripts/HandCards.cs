using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCards : MonoBehaviour{
    [SerializeField] private List<Transform> _handPlaces;
    [SerializeField] private List<Transform> _freePlacesInHand;
    [SerializeField] private int _handSize;

    [Header("Prefabs")]
    [SerializeField] private ArcaneCard _arcanePrefab;
    [SerializeField] private MonsterCard _monsterPrefab;
    private CardCreator _cardCreator;
    private Deck _deck;

    private void Awake() {
        _cardCreator = CardsDatabase.Instance.GetComponent<CardCreator>();
        _deck = GetComponentInParent<Deck>();
    }
    
    private void Start() {
        StartCoroutine(DrawCards());
    }

    private void VerifyOccupiedPlacesInHand(){
        _freePlacesInHand.Clear();
        foreach(var place in _handPlaces){
            HandPlacement handPlacement = place.GetComponent<HandPlacement>();
            if(handPlacement.Ocuppied == false){
                _freePlacesInHand.Add(place);
            }
        }
    }

    private bool HasfreePositionInHand(out int pos){
        pos = 0;
        if (_freePlacesInHand.Count >= 1){
            while (_freePlacesInHand[pos].GetComponent<HandPlacement>().Ocuppied == true && pos < 4){
                pos++;
            }
            return true;
        }else{
            return false;
        }
    }
    private IEnumerator DrawCards(){
        Debug.Log("Starting Draw Routine");
        VerifyOccupiedPlacesInHand();
        for(int i = 0; i < _handSize; i++){
            if (HasfreePositionInHand(out int pos)){
                int randomIndex = Random.Range(0, _deck.DeckBase.Count);
                Card newCard = _cardCreator.CreateCard(_deck.DeckBase[randomIndex]);
                newCard.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                Instantiate(newCard, _freePlacesInHand[pos].position, _freePlacesInHand[pos].rotation);
                HandPlacement handPlacement = _freePlacesInHand[pos].GetComponent<HandPlacement>();

                handPlacement.SetOccupation(true);
                _cardCreator.RemoveCreatedCardFromDeck(_deck.DeckBase, _deck.DeckBase[randomIndex]);
            }else{
                Debug.Log("No Free Positions in Hand");
            }
            yield return new WaitForSeconds(0.2f);        
        }
        VerifyOccupiedPlacesInHand();
    }
}
