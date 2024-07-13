using UnityEngine;

public class EndPhase : AbstractState{
    public override void Enter(){
        Debug.Log("Enter End Phase");
    }

    public override void Exit(){

    }

    public override string ToString(){
        return "End Phase";
    }
}