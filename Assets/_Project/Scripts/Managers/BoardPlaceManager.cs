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
        if(monstersInEnemyBoard != null){
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

    public List<BoardCardMonsterPlace> GetP2MonsterPlaces(){
        var monstersInP2Board = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;
        if(monstersInP2Board != null){
            foreach (var monsterPlace in monstersInP2Board){
                if (!monsterPlace.IsFree()){
                    monstersInP2Board.Add(monsterPlace);
                }
            }
        }
        return monstersInP2Board;
    }

    public (List<BoardCardMonsterPlace>, List<BoardCardMonsterPlace>) GetOcuppiedMonsterPlacesAI(){
        List<BoardCardMonsterPlace> P1MonsterPlaces;
        List<BoardCardMonsterPlace> P2MonsterPlaces;

        P1MonsterPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;
        P2MonsterPlaces = BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements;

        foreach(var place in P1MonsterPlaces){
            if(place.IsFree()){
                P1MonsterPlaces.Remove(place);
            }
        }

        foreach(var place in P2MonsterPlaces){
            if(place.IsFree()){
                P2MonsterPlaces.Remove(place);
            }
        }

        return (P1MonsterPlaces, P2MonsterPlaces);
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

    /*when the player1 attacks a player2 monster in defense position the turn is of the player 1
    that way, the rotation of the monster2, if not destroyed, was inverted. This function resolves that checking the card, not the turn.
    Dont implemented in all situations because the sintax is not that elegant (card.RotateCard...(card))*/
    public Quaternion DefenseFaceUpRotation(Card card){
        Quaternion newRotation;
        if(card.IsPlayerCard()){
            newRotation = Quaternion.Euler(90, 0, -90);
        }else{
            newRotation = Quaternion.Euler(90, 0, 90);
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