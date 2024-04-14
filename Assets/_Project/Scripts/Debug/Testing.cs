using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour {

    public static Testing Instance;

    [Header("Cards In Hand")]
    [SerializeField] private List<CardMonster> _lvl1MonstersList;
    [SerializeField] private List<CardMonster> _lvl2MonstersList;
    [SerializeField] private List<CardMonster> _lvl3MonstersList;
    [SerializeField] private List<CardArcane> _trapsList;
    [SerializeField] private List<CardArcane> _fieldsList;
    [SerializeField] private List<CardArcane> _equipsList;
    
    [Header("Cards On Field")]
    [SerializeField] private List<CardMonster> _lvl4MonstersList;
    [SerializeField] private List<CardMonster> _lvl5MonstersList;
    [SerializeField] private List<CardMonster> _lvl6MonstersList;
    [SerializeField] private List<CardMonster> _lvl7MonstersList;

    [SerializeField] private List<CardMonster> _AIMonstersOnField;
    [SerializeField] private List<Card> _faceUpAIMonsters;
    [SerializeField] private List<Card> _faceDownAIMonsters;
    [SerializeField] private List<Card> _faceUpPlayerMonsters;
    [SerializeField] private List<Card> _faceDownPlayerMonsters;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }else{
            Debug.LogError("More than on instance of TESTING.CS");
            Destroy(Instance);
            Instance = this;
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if(Input.GetKeyDown(KeyCode.F)){
            if(BattleManager.Instance.BattleStateManager.CurrentPhase == BattleManager.Instance.CardSelectionPhase &&
                BattleManager.Instance.CardSelector.GetSelectedCards().Count > 0){
                
                BattleManager.Instance.CardSelectionPhase.EndSelection();
            }
        }
    }

    public void UpdateLists(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList, List<CardMonster> lvl4MonstersList, List<CardMonster> lvl5MonstersList, List<CardMonster> lvl6MonstersList, List<CardMonster> lvl7MonstersList, List<CardArcane> trapsList, List<CardArcane> fieldsList, List<CardArcane> equipsList, List<CardMonster> AIMonstersOnField, List<Card> faceUpAIMonsters, List<Card> faceDownAIMonsters, List<Card> faceUpPlayerMonsters, List<Card> faceDownPlayerMonsters){

        //Hand
        _lvl1MonstersList = lvl1MonstersList;
        _lvl2MonstersList = lvl2MonstersList;
        _lvl3MonstersList = lvl3MonstersList;
        _trapsList = trapsList ;
        _fieldsList = fieldsList ;
        _equipsList = equipsList ;

        //Field
        _lvl4MonstersList = lvl4MonstersList;
        _lvl5MonstersList = lvl5MonstersList;
        _lvl6MonstersList = lvl6MonstersList;
        _lvl7MonstersList = lvl7MonstersList;


        //AI
        _AIMonstersOnField = AIMonstersOnField;
        _faceUpAIMonsters = faceUpAIMonsters;
        _faceDownAIMonsters = faceDownAIMonsters;

        //Player
        _faceUpPlayerMonsters = faceUpPlayerMonsters;
        _faceDownPlayerMonsters = faceDownPlayerMonsters;
    }
}