using UnityEngine;

public class BattleManager : MonoBehaviour {
    public static BattleManager Instance;
    
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private FusionManager _fusionManager;
    [SerializeField] private UIBattleManager _uiBattleManager;
    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private BoardPlaceManager _boardPlaceManager;
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private BattlePhaseStateManager _battleStateManager;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private ColorManager _colorManager;
    [SerializeField] private AIStateManager _AIStateManager;
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private ActionsManager _actionsManager;
    [SerializeField] private BattleFieldManager _batteFieldManager;

    [Header("")]
    [SerializeField] private HandPlayer _handPlayer;
    [SerializeField] private HandEnemy _handEnemy;


    //Public Refs//

    //Health
    public HealthManager HealthManager => _healthManager;

    //UI
    public UIBattleManager UIBattleManager => _uiBattleManager;

    //Camera
    public CameraManager CameraManager => _cameraManager;

    //Colors
    public ColorManager ColorManager => _colorManager;

    //Turn
    public TurnManager TurnManager => _turnManager;

    //Card
    public CardManager CardManager => _cardManager;
    public CardCreator CardCreator => _cardManager.CardCreator;
    public CardSelector CardSelector => _cardManager.CardSelector;
    public CardDatabase CardsDatabase => _cardManager.CardsDatabase;
    public CardVisuals CardVisuals => _cardManager.CardVisuals;
    public CardEffect CardEffect => _cardManager.CardEffect;

    //Fusion
    public Fusion Fusion => _fusionManager.Fusion;
    public FusionManager FusionManager => _fusionManager;
    public FusionPositions FusionPositions => _fusionManager.FusionPositions;
    public FusionMonster FusionMonster => _fusionManager.FusionMonster;
    public FusionArcane FusionArcane => _fusionManager.FusionArcane;
    public FusionEquip FusionEquip => _fusionManager.FusionEquip;

    //Board
    public BoardManager BoardManager => _boardManager;
    public BoardPlaceManager BoardPlaceManager => _boardPlaceManager;
    public BoardPlaceVisuals BoardPlaceVisuals => _boardPlaceManager.BoardPlaceVisuals;
    public PlayerBoardPlaces PlayerBoardPlaces => _boardPlaceManager.PlayerBoardPlaces;
    public EnemyBoardPlaces EnemyBoardPlaces => _boardPlaceManager.EnemyBoardPlaces;

    //Battle Field
    public BattleFieldManager BattleFieldManager => _batteFieldManager;

    //Battle State Machine
    public BattlePhaseStateManager BattleStateManager => _battleStateManager;
    public BattlePhaseStart StartPhase => _battleStateManager.BattlePhaseStart;
    public BattlePhaseDraw DrawPhase => _battleStateManager.BattlePhaseDraw;
    public BattlePhaseCardSelection CardSelectionPhase => _battleStateManager.BattlePhaseCardSelection;
    public BattlePhaseFusion FusionPhase => _battleStateManager.BattlePhaseFusion;
    public BattlePhaseSelections SelectionsPhase => _battleStateManager.BattlePhaseSelections;
    public BattlePhaseBoardPlaceSelection BoardPlaceSelectionPhase => _battleStateManager.BattleBoardSelectionPhase;
    public BattlePhaseAction ActionBattlePhase => _battleStateManager.BattlePhaseAction;
    public BattlePhaseAttack AttackPhase => _battleStateManager.BattlePhaseAttack;
    public BattlePhaseEnd EndPhase => _battleStateManager.BattlePhaseEnd;

    //Actions
    public ActionsManager ActionsManager => _actionsManager;
    
    //AI State Machine
    public AIStateManager AIManager => _AIStateManager;
    public AIStateStandBy AIStandBy => _AIStateManager.AIStandby;
    public AIStateCardSelection AICardSelection => _AIStateManager.AICardSelection;
    
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
        _boardManager = GetComponentInChildren<BoardManager>();
        _boardPlaceManager = GetComponentInChildren<BoardPlaceManager>();
        _battleStateManager = GetComponentInChildren<BattlePhaseStateManager>();
        _colorManager = GetComponentInChildren<ColorManager>();
        _cameraManager = GetComponentInChildren<CameraManager>();
        _AIStateManager = GetComponentInChildren<AIStateManager>();
        _healthManager = GetComponentInChildren<HealthManager>();
        _actionsManager = GetComponentInChildren<ActionsManager>();
        _batteFieldManager = GetComponentInChildren<BattleFieldManager>();
    }
}