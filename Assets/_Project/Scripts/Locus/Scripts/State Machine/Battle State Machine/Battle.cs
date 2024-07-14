using UnityEngine;

public class Battle : MonoBehaviour {
    public BattleManagerSO BattleManager;
    public FusionEventHandlerSO FusionManager;
    public BoardPlaceEventHandlerSO BoardManager;
    public CardStatEventHandlerSO CardStatSelManager;

    public PlayerHandManagerSO PlayerHandManager;
    public EnemyHandManagerSO EnemyHandManager;

    public UIEventHandlerSO UIManager;
    public CardManagerSO CardManager;
    public TurnManagerSO TurnManager;

    public AIManagerSO AIManager;


    public AbstractState CurrentState {get; private set;}
        
    public StartPhase StartPhase {get; private set;}
    public DrawPhase DrawPhase {get; private set;}
    public CardSelectionPhase CardSelection {get; private set;}
    public FusionPhase Fusion {get; private set;}
    public CardStatSelectPhase CardStatSelection {get; private set;}
    public BoardPlaceSelectionPhase BoardPlaceSelection {get; private set;}
    public ActionPhase Action {get; private set;}
    public ActionPhaseTwo ActionTwo {get; private set;}
    public EndPhase EndPhase {get; private set;}


    public Battle(){
        StartPhase = new();
        DrawPhase = new();
        CardSelection = new();
        Fusion = new();
        CardStatSelection = new();
        BoardPlaceSelection = new();
        Action = new();
        ActionTwo = new();
        EndPhase = new();
    }

    private void Start(){

        ChangeState(StartPhase);
    }

    public void ChangeState(AbstractState newState){
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.SetController(this);
        CurrentState.SetController(AIManager.AI);
        BattleManager.ChangeState(CurrentState);
        CurrentState.Enter();
    }
}