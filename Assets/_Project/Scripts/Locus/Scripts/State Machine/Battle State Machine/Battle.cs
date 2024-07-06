using System;
using UnityEngine;

public class Battle : StateManager {
    public static Action<AbstractState> OnStateChange;
    
    public StartPhase StartPhase {get; private set;}
    public DrawPhase DrawPhase {get; private set;}
    public CardSelectionPhase CardSelection {get; private set;}
    public FusionPhase Fusion {get; private set;}
    public CardStatSelectPhase CardStatSelection {get; private set;}
    public BoardPlaceSelectionPhase BoardPlaceSelection {get; private set;}
    public ActionPhase Action {get; private set;}


    public Battle(){
        StartPhase = new();
        DrawPhase = new();
        CardSelection = new();
        Fusion = new();
        CardStatSelection = new();
        BoardPlaceSelection = new();
        Action = new();
    }

    private void Start(){
        ChangeState(StartPhase);
    }

    public override void ChangeState(AbstractState newState){
        base.ChangeState(newState);
        OnStateChange?.Invoke(newState);
    }

    public override void ResetStates(){
        Debug.Log("ResetStates Called");
        StartPhase = null;
        DrawPhase = null;
        CardSelection = null;
        Fusion = null;
        CardStatSelection = null;
        BoardPlaceSelection = null; 
    }
}