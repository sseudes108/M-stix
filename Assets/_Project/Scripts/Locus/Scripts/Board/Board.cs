using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] private TurnManagerSO _turnManager;
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

#region Unity Methods
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

        _aIManager.Actor.SetBoardPlaces(EnemyMonsterPlaces, EnemyArcanePlaces);
        _aIManager.SetBoard(this);
    }
#endregion

    /*
        Listen the event from Board Manager. 
        That event happens each turn from the second.
        -> Clear the cards on field lists and circle trough all the board places.
        -> Set the updated lists on the AIManager.
    */
    private void BattleManager_OnStartPhase(){
        if(_turnManager.CurrentTurn == 1){ return; }

        PlayerCardsOnField.Clear();
        AICardsOnField.Clear();     

        CheckCardsOnBoard(PlayerMonsterPlaces);
        CheckCardsOnBoard(PlayerArcanePlaces);
        CheckCardsOnBoard(EnemyMonsterPlaces);
        CheckCardsOnBoard(EnemyArcanePlaces);

        SetAIOnBoardLists();
    }

    //Called by the AIActor on ResetBoardFusion() after a board fusion so AI got the updated list to use for the rest of the turn
    public void ResetAIBoardOnList(){
        AICardsOnField.Clear();

        foreach(var place in EnemyMonsterPlaces){
            if(place.IsFree){ continue; }
            AddCardToInBoardList(place, place.CardInPlace);
        }

        foreach(var place in EnemyArcanePlaces){
            if(place.IsFree){ continue; }
            AddCardToInBoardList(place, place.CardInPlace);
        }

        _aIManager.Actor.CardOrganizer.SetAICardsOnField(AICardsOnField);
    }
    
    public void ResetPlayerBoardOnList(){
        PlayerCardsOnField.Clear();

        foreach(var place in PlayerMonsterPlaces){
            if(place.IsFree){ continue; }
            AddCardToInBoardList(place, place.CardInPlace);
        }

        foreach(var place in PlayerArcanePlaces){
            if(place.IsFree){ continue; }
            AddCardToInBoardList(place, place.CardInPlace);
        }

        _aIManager.Actor.CardOrganizer.SetPlayerCardsOnField(PlayerCardsOnField);
    }

    /*Set the card list to AI use in current Turn. 
        Called by this script on BattleManager_OnStartPhase()
    */
    private void SetAIOnBoardLists(){
        _aIManager.Actor.CardOrganizer.SetAICardsOnField(AICardsOnField);
        _aIManager.Actor.CardOrganizer.SetPlayerCardsOnField(PlayerCardsOnField);
    }

    /*Check the board place received to see if its free, if not, call the SetCardOptions() and AddCardToInBoardList(). 
        Called by this script on BattleManager_OnStartPhase()
    */
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

    /*Set the card options. Is called on the Start Phase by an event. Its sets the card to can flip or can attack after the turn it was placed on board.
        Called by this script on CheckCardsOnBoard();
    */
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

    //Add the cards that are on board to respective owner list. Called by this script on (CheckCardsOnBoard())
    private void AddCardToInBoardList(BoardPlace place, Card card){ 
        if(place.IsPlayerPlace){
            PlayerCardsOnField.Add(card);
        }else{
            AICardsOnField.Add(card);
        }
    }

    //Set the color for the board places. Called By the GameManager.cs on Start
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