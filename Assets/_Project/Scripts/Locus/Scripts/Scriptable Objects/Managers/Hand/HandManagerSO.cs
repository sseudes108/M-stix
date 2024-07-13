using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HandManagerSO", menuName = "Mistix/Manager/Hand/Hand Abstract", order = 0)]
public class HandManagerSO : ScriptableObject {
    public UnityEvent OnCardsDrew;
    
    [SerializeField] protected BattleManagerSO _battleManager;
    [SerializeField] private CardManagerSO _cardManager;

    private Transform[] _handPositions;
    public List<Transform> _freePositionsInHand = new();
    public List<Card> CardsInHand = new();

    public Hand _hand;
    public Deck _deck;

    public virtual void OnEnable() {
        OnCardsDrew ??= new UnityEvent();
    }

    public void CardsDrew() { OnCardsDrew?.Invoke(); }

    public void CheckFreeHandPositions(){
        _freePositionsInHand.Clear();
        foreach(var position in _handPositions){
            var handPosition = position.GetComponent<HandPosition>();
            if(handPosition.IsFree){
                _freePositionsInHand.Add(position);
                // handPosition.SetPlaceFree(false);
            }
        }
    }

    public void Draw(MonoBehaviour caller){
        caller.StartCoroutine(DrawRoutine());
    }

    private IEnumerator DrawRoutine(){
        foreach(var position in _freePositionsInHand){
            var randomCardData = _deck.DeckInUse[Random.Range(0, _deck.DeckInUse.Count)];
            _deck.RemoveCardFromDeck(randomCardData);
            yield return null;

            var newCard = Instantiate(_cardManager.Creator.CreateCard(randomCardData));
            newCard.transform.SetPositionAndRotation(_deck.transform.position, _deck.transform.rotation);
            if(_hand is PlayerHand) { newCard.IsPlayeCard(); }
            newCard.SetCardOnHand(true);
            yield return null;

            position.GetComponent<HandPosition>().SetPlaceFree(false);
            CardsInHand.Add(newCard);
            yield return null;

            newCard.MoveCard(position);
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        yield return null;
        CardsDrew();
    }

    public void SetHand(Hand hand){
        _hand = hand;
    }

    public void SetDeck(Deck deck){
        _deck = deck;
    }

    public void SetHandPositions(Transform[] handPositions){
        _handPositions = handPositions;
    }
}