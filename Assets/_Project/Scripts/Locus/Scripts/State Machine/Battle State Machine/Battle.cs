using System;

public class Battle : StateManager {
    public static Action<AbstractState> OnStateChange;
    
    public StartPhase StartPhase {get; private set;}
    public DrawPhase DrawPhase {get; private set;}
    public CardSelectionPhase CardSelection {get; private set;}
    public FusionPhase Fusion {get; private set;}
    
    public Battle(){
        StartPhase = new();
        DrawPhase = new();
        CardSelection = new();
        Fusion = new();
    }

    private void Start(){
        ChangeState(StartPhase);
    }

    public override void ChangeState(AbstractState newState){
        base.ChangeState(newState);
        OnStateChange?.Invoke(newState);
    }
}