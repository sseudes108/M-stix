using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] private BoardManagerSO _boardManager;
    [SerializeField] private BattleManagerSO _battleManager;

    [SerializeField] private AIManagerSO _aIManager;

    public List<BoardPlace> PlayerMonsterPlaces;
    public List<BoardPlace> PlayerArcanePlaces;
    public List<BoardPlace> EnemyMonsterPlaces;
    public List<BoardPlace> EnemyArcanePlaces;

    private List<Card> _playerCardsOnField = new();
    private List<Card> _aICardsOnField = new();
    
    [field:SerializeField] public Hand PlayerHand {get; private set;}
    [field:SerializeField] public Hand EnemyHand {get; private set;}

    public BoardPlaceVisualController BoardPlaceVisualController;

    private void OnEnable() {
        BoardPlaceVisualController ??= new(PlayerMonsterPlaces, PlayerArcanePlaces, EnemyMonsterPlaces, EnemyArcanePlaces);
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    private void OnDisable() {
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    private void Start(){
        _boardManager.SetBoardPlaceVisualController(BoardPlaceVisualController);
        _aIManager.Actor.SetBoardPlaces(EnemyMonsterPlaces, EnemyArcanePlaces);
    }

    private void BattleManager_OnStartPhase(){
        _playerCardsOnField.Clear();
        _aICardsOnField.Clear();

        CheckCardsOnBoard(PlayerMonsterPlaces);
        CheckCardsOnBoard(PlayerArcanePlaces);
        CheckCardsOnBoard(EnemyMonsterPlaces);
        CheckCardsOnBoard(EnemyArcanePlaces);

        SetAIOnBoardLists();
    }

    private void SetAIOnBoardLists(){
        _aIManager.AI.SetAIOnBoardLists(_aICardsOnField, _playerCardsOnField);
    }

    private void CheckCardsOnBoard(List<BoardPlace> places){
        foreach(var place in places){
            if(place.IsFree) {continue;}

            SetCardOptions(place, place.CardInPlace);
            AddCardToInBoardList(place, place.CardInPlace);
        }
    }

    private void SetCardOptions(BoardPlace place, Card card){
        if(card is MonsterCard){
            (card as MonsterCard).SetCanChangeMode(true);
            (card as MonsterCard).SetCanAttack(true);
        }else{
            // Arcane Options
        }
        
        if(place.CardInPlace.IsFaceDown){
            place.CardInPlace.SetCanFlip();
        }
    }

    private void AddCardToInBoardList(BoardPlace place, Card card){
        if(place.IsPlayerPlace){
            _playerCardsOnField.Add(card);
        }else{
            _aICardsOnField.Add(card);
        }
    }
}