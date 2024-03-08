using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceManager : MonoBehaviour {
    [SerializeField] private BoardPlaceVisuals _boardPlaceVisuals;
    [SerializeField] private PlayerBoardPlaces _playerBoardPlaces;
    [SerializeField] private EnemyBoardPlaces _enemyBoardPlaces;

    private void Awake() {
        _boardPlaceVisuals = GetComponent<BoardPlaceVisuals>();
        _playerBoardPlaces = GetComponentInChildren<PlayerBoardPlaces>();
        _enemyBoardPlaces = GetComponentInChildren<EnemyBoardPlaces>();
    }

    public BoardPlaceVisuals BoardPlaceVisuals => _boardPlaceVisuals;
    public PlayerBoardPlaces PlayerBoardPlaces => _playerBoardPlaces;
    public EnemyBoardPlaces EnemyBoardPlaces => _enemyBoardPlaces;

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
            Debug.Log("T2Atk");
            newRotation = Quaternion.Euler(90, 0, -90);
        }else{
            Debug.Log("TAtk");
            newRotation = Quaternion.Euler(54, 54, 54);
        }
        return newRotation;
    }
    public Quaternion DefenseFaceDownRotation(){
        Quaternion newRotation;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            newRotation = Quaternion.Euler(-90, -180, -90);
        }else{
            Debug.Log("T2Def");
            Debug.Log("TDef");
            newRotation = Quaternion.Euler(54, 54, 54);
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
            var monsterPlace = place.GetComponentInChildren<BoardCardArcanePlace>();
            if (!monsterPlace.IsFree()){
                arcanesOnField.Add(monsterPlace);
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
}