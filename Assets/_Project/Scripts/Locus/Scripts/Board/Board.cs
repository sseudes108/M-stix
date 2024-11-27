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

    public List<Card> PlayerCardsOnField { get; private set; } = new();
    public List<Card> AICardsOnField { get; private set; } = new();

    public BoardPlaceVisualController BoardPlaceVisualController;

    private void OnEnable() {
        BoardPlaceVisualController ??= new(PlayerMonsterPlaces, PlayerArcanePlaces, EnemyMonsterPlaces, EnemyArcanePlaces);
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    private void OnDisable() {
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    private void Start(){
        _boardManager.SetBoardController(this);
        _boardManager.SetBoardPlaceVisualController(BoardPlaceVisualController);
        // _aIManager.Actor.SetBoardPlaces(EnemyMonsterPlaces, EnemyArcanePlaces);
    }

    private void BattleManager_OnStartPhase(){
        PlayerCardsOnField.Clear();
        AICardsOnField.Clear();

        CheckCardsOnBoard(PlayerMonsterPlaces);
        CheckCardsOnBoard(PlayerArcanePlaces);
        CheckCardsOnBoard(EnemyMonsterPlaces);
        CheckCardsOnBoard(EnemyArcanePlaces);

        SetAIOnBoardLists();
    }

    private void SetAIOnBoardLists(){
        _aIManager.Actor.CardOrganizer.SetAICardsOnField(AICardsOnField);
        _aIManager.Actor.CardOrganizer.SetPlayerCardsOnField(PlayerCardsOnField);
    }

    private void CheckCardsOnBoard(List<BoardPlace> places){
        foreach(var place in places){
            if(place.IsFree) {continue;}

            SetCardOptions(place, place.CardInPlace);
            AddCardToInBoardList(place, place.CardInPlace);
            if(place.CardInPlace.WasFlipedThisTurn){
                place.CardInPlace.SetWasFlipedThisTurn(false);
            }
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
            place.CardInPlace.SetCanFlip(true);
        }

        place.CardInPlace.SetShowButtons(true);
    }

    private void AddCardToInBoardList(BoardPlace place, Card card){
        if(place.IsPlayerPlace){
            PlayerCardsOnField.Add(card);
        }else{
            AICardsOnField.Add(card);
        }
    }

    public void SetColor(Vector3 playerColor, Vector3 enemyColor){
        foreach(var place in PlayerMonsterPlaces){
            place.Visual.SetPlaceColors(playerColor, enemyColor);
        }
        
        foreach(var place in PlayerArcanePlaces){
            place.Visual.SetPlaceColors(playerColor, enemyColor);
        }

        foreach(var place in EnemyMonsterPlaces){
            place.Visual.SetPlaceColors(playerColor, enemyColor);
        }

        foreach(var place in EnemyArcanePlaces){
            place.Visual.SetPlaceColors(playerColor, enemyColor);
        }
    }
}