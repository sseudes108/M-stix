using System.Collections;
using UnityEngine;

public class AISelectCardState : AbstractState{
    public AISelectCardState(StateMachine controller) : base(controller){}

    public override void Enter(){
        // Debug.Log("AISelectCardState Enter");
        StateMachine.AI.StartCoroutine(AIRoutine());
    }

    public override void Exit(){}

    public IEnumerator AIRoutine(){
        // Debug.Log("AIRoutine()");
        yield return StateMachine.AI.StartCoroutine(StateMachine.AI.Actor.CardSelector.SelectCardRoutine(StateMachine.AI.Manager.CardsInHand));
        yield return null;
    }

    public override string ToString(){
        return "Select Card";
    }
}