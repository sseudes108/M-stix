// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HandMovement))]
public class Hand : MonoBehaviour {
    [SerializeField] protected HandManagerSO _handManager;
    [SerializeField] protected BattleManagerSO _battleManager;
    [SerializeField] protected Transform[] _handPositions;
    [SerializeField] private Deck _deck;
    protected HandMovement _movement;

    public virtual void OnEnable() {
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    public virtual void OnDisable() {
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    public virtual void BattleManager_OnStartPhase(){
        // Debug.Log($"Hand {this} - BattleManager_OnStartPhase()");
        _handManager.CheckFreeHandPositions();
    }

    public virtual void Awake() {
        _movement = GetComponent<HandMovement>();
    }

    private void Start(){
        _handManager.SetHand(this);
        _handManager.SetDeck(_deck);
        _handManager.SetHandPositions(_handPositions);
    }
}