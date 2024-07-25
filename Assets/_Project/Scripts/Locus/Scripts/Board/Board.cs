using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] private BoardManagerSO _boardManager;
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private AIActorSO _aIActor;


    public List<BoardPlace> PlayerMonsterPlaces;
    public List<BoardPlace> PlayerArcanePlaces;
    public List<BoardPlace> EnemyMonsterPlaces;
    public List<BoardPlace> EnemyArcanePlaces;

    public BoardPlaceVisualController BoardPlaceVisualController;

    private void OnEnable() {
        BoardPlaceVisualController ??= new(PlayerMonsterPlaces, PlayerArcanePlaces, EnemyMonsterPlaces, EnemyArcanePlaces);
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }
    private void OnDisable() {
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    private void BattleManager_OnStartPhase(){
        CheckCardsOnBoard(PlayerMonsterPlaces);
        CheckCardsOnBoard(PlayerArcanePlaces);
        CheckCardsOnBoard(EnemyMonsterPlaces);
        CheckCardsOnBoard(EnemyArcanePlaces);
    }

    private void CheckCardsOnBoard(List<BoardPlace> places){
        foreach(var place in places){
            // Tester.Instance.CheckCall("Board", "foreach(var place in places) - Before if(place.IsFree){continue;}", "yellow");
            if(place.IsFree){continue;}
            // Tester.Instance.CheckCall("Board", "foreach(var place in places) - After if(place.IsFree){continue;}", "yellow");
            if(place.CardInPlace is MonsterCard){
                var monster = place.CardInPlace as MonsterCard;
                if(place.CardInPlace.IsFaceDown){
                    place.CardInPlace.SetCanFlip();
                }
                monster.SetCanChangeMode(true);
                monster.SetCanAttack(true);
                // Tester.Instance.CheckCall("Board", "foreach(var place in places) - place.Card is MonsterCard", "red");
                continue;
            }
        }
    }

    private void Start(){
        _boardManager.SetBoardPlaceVisualController(BoardPlaceVisualController);
        _aIActor.SetBoardPlaces(EnemyMonsterPlaces, EnemyArcanePlaces);
    }
}