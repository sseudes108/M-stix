public class Battle : StateManager {
    public BattleEventHandlerSO BattleManager;
    public FusionEventHandlerSO FusionManager;
    public BoardPlaceEventHandlerSO BoardManager;
    public CardStatEventHandlerSO CardStatSelManager;
    public HandEventHandlerSO HandManager;
    public UIEventHandlerSO UIManager;
    public BattleHelperSO Helper;
    
    
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
        BattleManager.SetBattle(this);
        ChangeState(StartPhase);
    }

    public override void ChangeState(AbstractState newState){
        base.ChangeState(newState);
        BattleManager.ChangeState(newState);
    }
}