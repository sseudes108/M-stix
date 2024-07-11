using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HandMovement))]
public abstract class Hand : MonoBehaviour {
    public BattleEventHandlerSO BattleManager;
    public HandEventHandlerSO HandManager;

    [SerializeField] private Transform[] _handPositions;
    [SerializeField] private List<Transform> _freePositionsInHand;
    private Deck _deck;

    protected HandMovement _movement;

    public virtual void OnEnable() {
        BattleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    public virtual void OnDisable() {
        BattleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    public virtual void BattleManager_OnStartPhase(){
        CheckFreeHandPositions();
    }

    public virtual void Awake() {
        _deck = GetComponentInChildren<Deck>();
        _movement = GetComponent<HandMovement>();
    }

    private void CheckFreeHandPositions(){
        foreach(var position in _handPositions){
            var handPosition = position.GetComponent<HandPosition>();
            if(handPosition.IsFree){
                _freePositionsInHand.Add(position);
                handPosition.IsPlaceFree(false);
            }
        }
    }

    public void Draw(){
        StartCoroutine(DrawRoutine());
    }

    private IEnumerator DrawRoutine(){
        foreach(var position in _freePositionsInHand){

            var randomCardData = _deck.DeckInUse[UnityEngine.Random.Range(0,_deck.DeckInUse.Count - 1)];
            _deck.RemoveCardFromDeck(randomCardData);
            yield return null;

            var newCard = Instantiate(GameManager.Instance.CardManager.Creator.CreateCard(randomCardData));
            newCard.transform.SetPositionAndRotation(_deck.transform.position, _deck.transform.rotation);
            if(this is PlayerHand) { newCard.IsPlayeCard(); }
            newCard.SetCardOnHand(true);
            yield return null;
            newCard._battleManager = BattleManager;
            newCard.MoveCard(position);
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        yield return null;
        HandManager.CardsDrew();
    }
}