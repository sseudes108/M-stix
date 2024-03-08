using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceManager : MonoBehaviour {
    [SerializeField] private BoardPlaceVisuals _boardPlaceVisuals;
    [SerializeField] private PlayerBoardPlaces _playerBoardPlaces;
    [SerializeField] private EnemyBoardPlaces _enemyBoardPlaces;
    private Card _lastCardPlaced;

    private void Awake() {
        _boardPlaceVisuals = GetComponent<BoardPlaceVisuals>();
        _playerBoardPlaces = GetComponentInChildren<PlayerBoardPlaces>();
        _enemyBoardPlaces = GetComponentInChildren<EnemyBoardPlaces>();
    }

    public BoardPlaceVisuals BoardPlaceVisuals => _boardPlaceVisuals;
    public PlayerBoardPlaces PlayerBoardPlaces => _playerBoardPlaces;
    public EnemyBoardPlaces EnemyBoardPlaces => _enemyBoardPlaces;

    public Card GetLastPlacedCard(){
        return _lastCardPlaced;
    }

    public void SetLastCardPlaced(Card card){
        _lastCardPlaced = null;
        _lastCardPlaced = card;
    }

    //Card positions on board
    //Arcane Cards
    public Quaternion FaceDownRotation(){
        return Quaternion.Euler(-90, -180, 0);
    } 
    public Quaternion FaceUpRotation(){
        return Quaternion.Euler(90, 0, 0);
    } 

    //Monster Cards
    public Quaternion AttackFaceUpRotation(){
        Quaternion newRotation;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            newRotation = Quaternion.Euler(90, 0, 0);
        }else{
            newRotation = Quaternion.Euler(90, 0, -180);
        }
        return newRotation;
    }
    public Quaternion AttackFaceDownRotation(){
        Quaternion newRotation;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            newRotation = Quaternion.Euler(-90, -180, 0);
        }else{
            newRotation = Quaternion.Euler(-90, -180, -180);
        }
        return newRotation;
    }
    public Quaternion DefenseFaceUpRotation(){
        Quaternion newRotation;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            newRotation = Quaternion.Euler(90, 0, -90);
        }else{
            newRotation = Quaternion.Euler(90, 0, 90);
        }
        return newRotation;
    }
    public Quaternion DefenseFaceDownRotation(){
        Quaternion newRotation;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            newRotation = Quaternion.Euler(-90, -180, -90);
        }else{
            newRotation = Quaternion.Euler(-90, -180, 90);
        }
        return newRotation;
    } 

    //Check cards on Board
    private List<BoardCardMonsterPlace> CheckMonstersOnField(){
        List<Transform> monsterPlaces;
        List<BoardCardMonsterPlace> monstersOnField = new();

        if (BattleManager.Instance.TurnManager.IsPlayerTurn()){
            monsterPlaces = BattleManager.Instance.PlayerBoardPlaces.MonsterPlaces;

        }else{
            monsterPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlaces;
        }

        foreach (var place in monsterPlaces){
            var monsterPlace = place.GetComponentInChildren<BoardCardMonsterPlace>();
            if (!monsterPlace.IsFree()){
                monstersOnField.Add(monsterPlace);
            }
        }

        return monstersOnField;
    }

    private List<BoardCardArcanePlace> CheckArcanesOnField(){
        List<Transform> arcanePlaces;
        List<BoardCardArcanePlace> arcanesOnField = new();

        if (BattleManager.Instance.TurnManager.IsPlayerTurn()){
            arcanePlaces = BattleManager.Instance.PlayerBoardPlaces.ArcanePlaces;

        }else{
            arcanePlaces = BattleManager.Instance.EnemyBoardPlaces.ArcanePlaces;

        }

        foreach (var place in arcanePlaces){
            var arcanePlace = place.GetComponentInChildren<BoardCardArcanePlace>();
            if (!arcanePlace.IsFree()){
                arcanesOnField.Add(arcanePlace);
            }
        }

        return arcanesOnField;
    }

    public void DisableOnBoardCardColliders(){
        var monstersOnField = CheckMonstersOnField();
        var arcanesOnField = CheckArcanesOnField();
        foreach(var monster in monstersOnField){
            monster.DisableCardColliderInBoardPhaseSelection();
        }
        foreach(var arcane in arcanesOnField){
            arcane.DisableCardColliderInBoardPhaseSelection();
        }
    }

    public void EnableOnBoardCardColliders(){
        var monstersOnField = CheckMonstersOnField();
        var arcanesOnField = CheckArcanesOnField();
        foreach(var monster in monstersOnField){
            monster.EnableCardColliderInBoardPhaseSelection();
        }
        foreach(var arcane in arcanesOnField){
            arcane.EnableCardColliderInBoardPhaseSelection();
        }
    }

    public List<CardMonster> GetAllMonstersOnTheField(){
        List<BoardCardMonsterPlace> allMonstersPlacesOccupied = new();

        var playerMonstersPlaces = BattleManager.Instance.PlayerBoardPlaces.MonsterPlaces;
        foreach (var place in playerMonstersPlaces){
            var monsterPlace = place.GetComponentInChildren<BoardCardMonsterPlace>();
            if (!monsterPlace.IsFree()){
                allMonstersPlacesOccupied.Add(monsterPlace);
            }
        }

        var enemyMonstersPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlaces;
        foreach (var place in enemyMonstersPlaces){
            var monsterPlace = place.GetComponentInChildren<BoardCardMonsterPlace>();
            if (!monsterPlace.IsFree()){
                allMonstersPlacesOccupied.Add(monsterPlace);
            }
        }
        
        List<CardMonster> allMonstersOnTheField = new();
        foreach(var place in allMonstersPlacesOccupied){
            var card = place.GetComponentInChildren<CardMonster>();
            allMonstersOnTheField.Add(card);
        }

        return allMonstersOnTheField;
    }

    public void RemoveCardFromBoard(Card card){
        var place = card.GetComponentInParent<BoardCardPlacement>();
        place.SetPlaceFree();
    }
}