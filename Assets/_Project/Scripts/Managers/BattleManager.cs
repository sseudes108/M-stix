using UnityEngine;

public class BattleManager : MonoBehaviour {
    public static BattleManager Instance;
    
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private FusionManager _fusionManager;
    [SerializeField] private UIBattleManager _uiBattleManager;
    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private BoardPlaceManager _boardPlaceManager;
    [SerializeField] private BattlePhaseStateManager _battleStateManager;

    [SerializeField] private HandPlayer _handPlayer;
    [SerializeField] private HandEnemy _handEnemy;

    //Public Refs//

    //UI
    public UIBattleManager UIBattleManager => _uiBattleManager;

    //Turn
    public TurnManager TurnManager => _turnManager;

    //Card
    public CardCreator CardCreator => _cardManager.CardCreator;
    public CardSelector CardSelector => _cardManager.CardSelector;
    public CardDatabase CardsDatabase => _cardManager.CardsDatabase;
    public CardVisuals CardVisuals => _cardManager.CardVisuals;

    //Fusion
    public Fusion Fusion => _fusionManager.Fusion;
    public FusionPositions FusionPositions => _fusionManager.FusionPositions;
    public FusionMonster FusionMonster => _fusionManager.FusionMonster;
    public FusionArcane FusionArcane => _fusionManager.FusionArcane;
    public FusionEquip FusionEquip => _fusionManager.FusionEquip;

    //Board
    public BoardPlaceVisuals BoardPlaceVisuals => _boardPlaceManager.BoardPlaceVisuals;
    public PlayerBoardPlaces PlayerBoardPlaces => _boardPlaceManager.PlayerBoardPlaces;
    public EnemyBoardPlaces EnemyBoardPlaces => _boardPlaceManager.EnemyBoardPlaces;


    //Battle State Machine
    public BattlePhaseStateManager BattleStateManager => _battleStateManager;
    public BattlePhaseStart StartPhase => _battleStateManager.BattlePhaseStart;
    public BattlePhaseDraw DrawPhase => _battleStateManager.BattlePhaseDraw;
    public BattlePhaseCardSelection CardSelectionPhase => _battleStateManager.BattlePhaseCardSelection;
    public BattlePhaseFusion FusionPhase => _battleStateManager.BattlePhaseFusion;
    public BattlePhaseFaceSelection FaceSelectionPhase => _battleStateManager.BattlePhaseFaceSelection;
    public BattlePhaseMonsterModeSelection MonsterModeSelectionPhase => _battleStateManager.BattlePhaseMonsterModeSelection;
    public BattlePhaseSelectAnima AnimaSelectionPhase => _battleStateManager.BattlePhaseSelectAnima;
    public BattlePhaseBoardPlaceSelection BoardPlaceSelectionPhase => _battleStateManager.BattleBoardSelectionPhase;
    public BattlePhaseAction ActionPhase => _battleStateManager.BattlePhaseAction;

    //Hand
    public HandPlayer PlayerHand => _handPlayer;
    public HandEnemy EnemyHand => _handEnemy;

    private void Awake(){
        SetSingleton();
        SetComponents();
    }

    private void SetSingleton(){
        if (Instance != null){
            Debug.Log("More than one instance of BattleManager");
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void SetComponents(){
        _cardManager = GetComponentInChildren<CardManager>();
        _fusionManager = GetComponentInChildren<FusionManager>();
        _uiBattleManager = GetComponentInChildren<UIBattleManager>();
        _turnManager = GetComponentInChildren<TurnManager>();
        _boardPlaceManager = GetComponentInChildren<BoardPlaceManager>();
        _battleStateManager = GetComponentInChildren<BattlePhaseStateManager>();
    }
}