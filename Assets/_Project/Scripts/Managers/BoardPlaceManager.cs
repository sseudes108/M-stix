using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceManager : MonoBehaviour {
    [SerializeField] private BoardPlaceVisuals _boardPlaceVisuals;

    [SerializeField] private PlayerBoardPlaces _playerBoardPlaces;
    [SerializeField] private EnemyBoardPlaces _enemyBoardPlaces;

    private Card _lastCardPlaced;

    public BoardPlaceVisuals BoardPlaceVisuals => _boardPlaceVisuals;
    public PlayerBoardPlaces PlayerBoardPlaces => _playerBoardPlaces;
    public EnemyBoardPlaces EnemyBoardPlaces => _enemyBoardPlaces;

    private void OnEnable() {
        BattleManager.Instance.TurnManager.OnTurnEnd += TurnSystem_OnTurnEnd;
    }

    private void OnDisable() {
        BattleManager.Instance.TurnManager.OnTurnEnd -= TurnSystem_OnTurnEnd;
    }

    private void Awake() {
        _boardPlaceVisuals = GetComponent<BoardPlaceVisuals>();
        _playerBoardPlaces = GetComponentInChildren<PlayerBoardPlaces>();
        _enemyBoardPlaces = GetComponentInChildren<EnemyBoardPlaces>();
    }

    private void TurnSystem_OnTurnEnd(bool IsPlayerTurn){
        if(IsPlayerTurn){
            foreach(var place in PlayerBoardPlaces.MonsterPlacements){
                place.ResetChangeModeAndAttack();
            }
        }else{
            foreach(var place in EnemyBoardPlaces.MonsterPlacements){
                place.ResetChangeModeAndAttack();
            }
        }
    }

    public Card GetLastPlacedCard(){
        return _lastCardPlaced;
    }

    public void SetLastCardPlaced(Card card){
        _lastCardPlaced = null;
        _lastCardPlaced = card;
    }


    //Check cards on Board
    public List<BoardCardMonsterPlace> CheckMonstersOnField(){
        List<BoardCardMonsterPlace> monstersOnField = new();

        if (BattleManager.Instance.TurnManager.IsPlayerTurn()){
            var boardCardMonsterPlace = BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements;
            foreach(var place in boardCardMonsterPlace){
                if(!place.IsFree()){
                    monstersOnField.Add(place);
                }
            }
        }else{
            var boardCardMonsterPlace = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;
            foreach(var place in boardCardMonsterPlace){
                if(!place.IsFree()){
                    monstersOnField.Add(place);
                }
            }
        }
        return monstersOnField;
    }

    private List<BoardCardArcanePlace> CheckArcanesOnField(){
        List<BoardCardArcanePlace> arcanesOnField = new();

        if (BattleManager.Instance.TurnManager.IsPlayerTurn()){
            var arcanePlacesOnField = BattleManager.Instance.PlayerBoardPlaces.ArcanePlacements;
        }else{
            var arcanePlacesOnField = BattleManager.Instance.EnemyBoardPlaces.ArcanePlacements;
        }

        foreach (var arcanePlace in arcanesOnField){
            if (!arcanePlace.IsFree()){
                arcanesOnField.Add(arcanePlace);
            }
        }

        return arcanesOnField;
    }

    public List<CardMonster> GetAllMonstersOnTheField(){
        List<BoardCardMonsterPlace> allMonstersPlacesOccupied = new();

        //Player monsters
        var monstersInPlayerBoard = BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements;
        if(monstersInPlayerBoard != null){
            foreach (var monsterPlace in monstersInPlayerBoard){
                if (!monsterPlace.IsFree()){
                    allMonstersPlacesOccupied.Add(monsterPlace);
                }
            }
        }

        //Enemy Monsters
        var monstersInEnemyBoard = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;
        if(monstersInPlayerBoard != null){
            foreach (var monsterPlace in monstersInEnemyBoard){
                if (!monsterPlace.IsFree()){
                    allMonstersPlacesOccupied.Add(monsterPlace);
                }
            }
        }
        
        //Merge
        List<CardMonster> allMonstersOnTheField = new();
        foreach(var place in allMonstersPlacesOccupied){
            var card = place.GetCardInThisPlace() as CardMonster;
            allMonstersOnTheField.Add(card);
        }

        return allMonstersOnTheField;
    }

    public List<BoardCardMonsterPlace> GetOcuppiedMonsterPlaces(){
        List<BoardCardMonsterPlace> ocuppiedMonsterPlaces = new();
        List<BoardCardMonsterPlace> monsterPlaces;

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            monsterPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;
        }else{
            monsterPlaces = BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements;
        }

        foreach(var place in monsterPlaces){
            if(!place.IsFree()){
                ocuppiedMonsterPlaces.Add(place);
            }
        }

        return ocuppiedMonsterPlaces;
    }

    public void RemoveCardFromBoard(Card card){
        var place = card.GetComponentInParent<BoardCardPlace>();
        place.SetPlaceFree();
    }

    #region Colliders
    // public void DisableOnBoardCardColliders(){
    //     var monstersOnField = CheckMonstersOnField();
    //     var arcanesOnField = CheckArcanesOnField();
    //     foreach(var monster in monstersOnField){
    //         monster.DisableCardColliderInBoardPhaseSelection();
    //     }
    //     foreach(var arcane in arcanesOnField){
    //         arcane.DisableCardColliderInBoardPhaseSelection();
    //     }
    // }

    // public void EnableOnBoardCardColliders(){
    //     var monstersOnField = CheckMonstersOnField();
    //     var arcanesOnField = CheckArcanesOnField();
    //     foreach(var monster in monstersOnField){
    //         monster.EnableCardColliderInBoardPhaseSelection();
    //     }
    //     foreach(var arcane in arcanesOnField){
    //         arcane.EnableCardColliderInBoardPhaseSelection();
    //     }
    // }
    #endregion

    #region Card Rotations
    //Card positions on board
    //Arcane Cards
    public Quaternion FaceDownRotation(){
        Quaternion newRotation;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            newRotation = Quaternion.Euler(-90, -180, 0);
        }else{
            newRotation = Quaternion.Euler(-90, -180, -180);
        }
        return newRotation;
    } 
    public Quaternion FaceUpRotation(){
        Quaternion newRotation;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            newRotation = Quaternion.Euler(90, 0, 0);
        }else{
            newRotation = Quaternion.Euler(90, 0, -180);
        }
        return newRotation;
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
    #endregion
}