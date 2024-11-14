using UnityEngine;

public class Battle : StateMachine {
    [Header("Battle System")]
    public FusionManagerSO FusionManager;
    public BoardManagerSO BoardManager;

    [Header("Hands")]
    public PlayerHandManagerSO PlayerHandManager;
    public EnemyHandManagerSO EnemyHandManager;
    
    [Header("Card")]
    public CardStatEventHandlerSO CardStatSelManager;
    public CardManagerSO CardManager;

    public AbstractState CurrentState { get; private set; }
    
    //States - Phases
    public StartPhase StartPhase { get; private set; }
    public DrawPhase DrawPhase { get; private set; }
    public CardSelectionPhase CardSelection { get; private set; }
    public FusionPhase Fusion { get; private set; }
    public CardStatSelectPhase CardStatSelection { get; private set; }
    public BoardPlaceSelectionPhase BoardPlaceSelection { get; private set; }
    public ActionPhase Action { get; private set; }
    public AttackSelectionPhase AttackSelectionPhase { get; private set; }
    public DamagePhase DamagePhase { get; private set; }
    public EndPhase EndPhase { get; private set; }

    public Battle(){
        StartPhase = new(this);
        DrawPhase = new(this);
        CardSelection = new(this);
        Fusion = new(this);
        CardStatSelection = new(this);
        BoardPlaceSelection = new(this);
        Action = new(this);
        AttackSelectionPhase = new(this);
        DamagePhase = new(this);
        EndPhase = new(this);
    }

    private void Start(){
        BattleManager.SetBattleController(this);
        ChangeState(StartPhase);
    }

    public void ChangeState(AbstractState newState){
        if(newState == CurrentState) { return;}

        CurrentState?.Exit();
        CurrentState = newState;
        
        BattleManager.ChangeState(CurrentState);
        CurrentState.Enter();

        TesterUI.Instance.UpdateBattlePhaseText(CurrentState.ToString());
    }
}