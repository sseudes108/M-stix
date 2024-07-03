// using UnityEngine;

// public class BattleManager : MonoBehaviour {
//     public static BattleManager Instance;
//     public CardManager CardManager {get; private set;}
//     public FusionManager FusionManager {get; private set;}
//     public UIBattleManager UIBattleManager {get; private set;}
//     public TurnManager TurnManager {get; private set;}
//     public BoardPlaceManager BoardPlaceManager {get; private set;}
//     public BoardManager BoardManager {get; private set;}
//     public BattleStateManager BattleStateManager {get; private set;}
//     public CameraManager CameraManager {get; private set;}
//     public ColorManager ColorManager {get; private set;}
//     public AIStateManager AIStateManager {get; private set;}
//     public AILib AILib {get; private set;}
//     public HealthManager HealthManager {get; private set;}
//     public ActionsManager ActionsManager {get; private set;}
//     public BattleFieldManager BatteFieldManager {get; private set;}
//     public VFXManager VFXManager {get; private set;}
//     public InputManager InputManager {get; private set;}


//     [Header("")]
//     [SerializeField] private HandPlayer _handPlayer;
//     [SerializeField] private HandEnemy _handEnemy;

// #region Card References

//     public CardCreator CardCreator => CardManager.CardCreator;
//     public CardSelector CardSelector => CardManager.CardSelector;
//     public CardDatabase CardsDatabase => CardManager.CardsDatabase;
//     public CardVisuals CardVisuals => CardManager.CardVisuals;
//     public CardEffect CardEffect => CardManager.CardEffect;

// #endregion

// #region Fusion References

//     public Fusion Fusion => FusionManager.Fusion;
//     public FusionPositions FusionPositions => FusionManager.FusionPositions;
//     public FusionMonster FusionMonster => FusionManager.FusionMonster;
//     public FusionArcane FusionArcane => FusionManager.FusionArcane;
//     public FusionEquip FusionEquip => FusionManager.FusionEquip;

// #endregion

// #region Board References

//     public BoardPlaceVisuals BoardPlaceVisuals => BoardPlaceManager.BoardPlaceVisuals;
//     public PlayerBoardPlaces PlayerBoardPlaces => BoardPlaceManager.PlayerBoardPlaces;
//     public EnemyBoardPlaces EnemyBoardPlaces => BoardPlaceManager.EnemyBoardPlaces;

// #endregion

// #region State Machine References

//     public BPStart StartPhase => BattleStateManager.StartPhase;
//     public BPDraw DrawPhase => BattleStateManager.DrawPhase;
//     public BPCardSelection CardSelectionPhase => BattleStateManager.CardSelectionPhase;
//     public BPFusion FusionPhase => BattleStateManager.FusionPhase;
//     public BPAfterFusionSelections AfterFusionSelections => BattleStateManager.AfterFusionSelections;
//     public BPBoardPlaceSelection BoardPlaceSelectionPhase => BattleStateManager.BoardPlaceSelectionPhase;
//     public BPAction ActionPhase => BattleStateManager.ActionPhase;
//     public BPAttack AttackPhase => BattleStateManager.AttackPhase;
//     public BPEnd EndPhase => BattleStateManager.EndPhase;

// #endregion
 
//  #region AI State Machine References
//     public AIStateStandBy AIStandBy => AIStateManager.AIStandby;
//     public AIStateCardSelection AICardSelection => AIStateManager.AICardSelection;
//     public AIStateAttack AIAttack => AIStateManager.AIAttack;

// #endregion
    
//     //Hand
//     public HandPlayer PlayerHand => _handPlayer;
//     public HandEnemy EnemyHand => _handEnemy;
    

//     private void Awake(){
//         SetSingleton();
//         SetComponents();
//     }

//     private void SetSingleton(){
//         if (Instance != null){
//             Debug.LogError("More than one instance of BattleManager");
//             Destroy(gameObject);
//         }
//         Instance = this;
//     }

//     private void SetComponents(){
//         CardManager = GetComponentInChildren<CardManager>();
//         FusionManager = GetComponentInChildren<FusionManager>();
//         UIBattleManager = GetComponentInChildren<UIBattleManager>();
//         TurnManager = GetComponentInChildren<TurnManager>();
//         BoardManager = GetComponentInChildren<BoardManager>();
//         BoardPlaceManager = GetComponentInChildren<BoardPlaceManager>();
//         BattleStateManager = GetComponentInChildren<BattleStateManager>();
//         ColorManager = GetComponentInChildren<ColorManager>();
//         CameraManager = GetComponentInChildren<CameraManager>();

//         AIStateManager = GetComponentInChildren<AIStateManager>();
//         AILib = GetComponentInChildren<AILib>();
        
//         HealthManager = GetComponentInChildren<HealthManager>();
//         ActionsManager = GetComponentInChildren<ActionsManager>();
//         BatteFieldManager = GetComponentInChildren<BattleFieldManager>();
//         VFXManager = GetComponentInChildren<VFXManager>();
//         InputManager = GetComponentInChildren<InputManager>();
//     }
// }