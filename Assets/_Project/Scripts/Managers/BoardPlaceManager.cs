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

    public List<BoardCardMonsterPlace> GetPlayerMonsterPlaces(){
        var playerMonstersOnBoard = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;

        foreach (var monsterPlace in playerMonstersOnBoard){
            if (!monsterPlace.IsFree()){
                playerMonstersOnBoard.Add(monsterPlace);
            }
        }
        return playerMonstersOnBoard;
    }
    public List<BoardCardMonsterPlace> GetAIMonsterPlaces(){
        var AIMonstersOnBoard = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;
        if(AIMonstersOnBoard != null){
            foreach (var monsterPlace in AIMonstersOnBoard){
                if (!monsterPlace.IsFree()){
                    AIMonstersOnBoard.Add(monsterPlace);
                }
            }
        }
        return AIMonstersOnBoard;
    }

# region Used on the AI only
    public (List<BoardCardMonsterPlace>, List<BoardCardMonsterPlace>) GetOcuppiedMonsterPlacesAI(){
        List<BoardCardMonsterPlace> playerMonsterPlaces = BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements;
        List<BoardCardMonsterPlace> aiMonsterPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;

        List<BoardCardMonsterPlace> playerOcupiedPlaces = new();
        List<BoardCardMonsterPlace> aiOcupiedPlaces = new();

        foreach(var place in playerMonsterPlaces){
            if(!place.IsFree()){
                playerOcupiedPlaces.Add(place);
            }
        }

        foreach(var place in aiMonsterPlaces){
            if(!place.IsFree()){
                aiOcupiedPlaces.Add(place);
            }
        }

        return (playerOcupiedPlaces, aiOcupiedPlaces);
    }
    public (List<BoardCardArcanePlace>, List<BoardCardArcanePlace>) GetOcuppiedArcanePlacesAI(){
        List<BoardCardArcanePlace> playerArcanePlaces = BattleManager.Instance.PlayerBoardPlaces.ArcanePlacements;
        List<BoardCardArcanePlace> aiArcanePlaces = BattleManager.Instance.EnemyBoardPlaces.ArcanePlacements;

        List<BoardCardArcanePlace> playerOcupiedPlaces = new();
        List<BoardCardArcanePlace> aiOcupiedPlaces = new();

        foreach(var place in playerArcanePlaces){
            if(!place.IsFree()){
                playerOcupiedPlaces.Add(place);
            }
        }

        foreach(var place in aiArcanePlaces){
            if(!place.IsFree()){
                aiOcupiedPlaces.Add(place);
            }
        }

        return (playerOcupiedPlaces, aiOcupiedPlaces);
    }
#endregion

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