using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour {

    public static Testing Instance;
    [SerializeField] private int _boardFusionLvl;
    [SerializeField] private List<CardMonster> _AIMonstersOnField;

    [Header("AI Cards In Hand")]
    [SerializeField] private List<CardMonster> _lvl1MonstersList;
    [SerializeField] private List<CardMonster> _lvl2MonstersList;
    [SerializeField] private List<CardMonster> _lvl3MonstersList;
    [SerializeField] private List<CardArcane> _trapsList;
    [SerializeField] private List<CardArcane> _fieldsList;
    [SerializeField] private List<CardArcane> _equipsList;
    
    [Header("AI Cards On Field")]

    [SerializeField] private List<CardMonster> _aiMonstersFaceUp;
    [SerializeField] private List<CardMonster> _aiMonstersFaceDown;
    [SerializeField] private List<CardArcane> _aiArcanesFaceUp;
    [SerializeField] private List<CardArcane> _aiArcanesFaceDown;

    [Header("AI Monsters On Field")]
    [SerializeField] private List<CardMonster> _lvl4MonstersList;
    [SerializeField] private List<CardMonster> _lvl5MonstersList;
    [SerializeField] private List<CardMonster> _lvl6MonstersList;
    [SerializeField] private List<CardMonster> _lvl7MonstersList;

    [Header("Player Cards on Field")]
    [SerializeField] private List<CardMonster> _playerMonstersFaceUp;
    [SerializeField] private List<CardMonster> _playerMonstersFaceDown;
    [SerializeField] private List<CardArcane> _playerArcanesFaceUp;
    [SerializeField] private List<CardArcane> _playerArcanesFaceDown;

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

    public void UpdateLists(AICardsList cardList){
        //Hand
        _lvl1MonstersList = cardList.Lvl1MonstersList;
        _lvl2MonstersList = cardList.Lvl2MonstersList;
        _lvl3MonstersList = cardList.Lvl3MonstersList;
        _trapsList = cardList.TrapsList ;
        _fieldsList = cardList.FieldsList ;
        _equipsList = cardList.EquipsList ;

        //Field
        _lvl4MonstersList = cardList.Lvl4MonstersList;
        _lvl5MonstersList = cardList.Lvl5MonstersList;
        _lvl6MonstersList = cardList.Lvl6MonstersList;
        _lvl7MonstersList = cardList.Lvl7MonstersList;

        //AI
        _aiMonstersFaceUp = cardList.AIMonstersFaceUp;
        _aiMonstersFaceDown = cardList.AIMonstersFaceDown;
        _aiArcanesFaceUp = cardList.AIArcanesFaceUp;
        _aiArcanesFaceDown = cardList.AIArcanesFaceDown;

        //Player
        _playerMonstersFaceUp = cardList.PlayerMonstersFaceUp;
        _playerMonstersFaceDown = cardList.PlayerMonstersFaceDown;
        _playerArcanesFaceUp = cardList.PlayerArcanesFaceUp;
        _playerArcanesFaceDown = cardList.PlayerArcanesFaceDown;
    }

    public void UpdateBoardFusionLvl(int lvl){
        _boardFusionLvl = lvl;
    }
}