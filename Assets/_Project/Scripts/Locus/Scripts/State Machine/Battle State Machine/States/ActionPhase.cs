using System;

public class ActionPhase : AbstractState{

    public static Action OnActionPhaseStart;

    public override void Enter(){
        OnActionPhaseStart?.Invoke();
    }

    public override void Exit(){

    }

    public override string ToString(){
        return "Action Phase";
    }
}