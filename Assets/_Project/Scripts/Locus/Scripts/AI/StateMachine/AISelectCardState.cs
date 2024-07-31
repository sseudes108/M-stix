using System.Collections;
using UnityEngine;

public class AISelectCardState : AbstractState{
    public AISelectCardState(StateMachine controller) : base(controller){}

    public override void Enter(){
        Debug.Log("AISelectCardState Enter");
        StateMachine.AI.StartCoroutine(AIRoutine());
    }

    public override void Exit(){}

    // public override void SubscribeEvents(){
    //     AI.Actor.CardSelector_OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
    // }

    // public override void UnsubscribeEvents(){
       
    //     AI.Actor.CardSelector_OnSelectionFinished.AddListener(AI_Actor_CardSelector_OnCardsSelected);
    // }

    // private void AI_Actor_CardSelector_OnCardsSelected() { ChangePhase(); }

    public IEnumerator AIRoutine(){
        Debug.Log("AIRoutine()");
        yield return StateMachine.AI.StartCoroutine(StateMachine.AI.Actor.CardSelector.SelectCardRoutine(StateMachine.AI.Manager.CardsInHand));
        yield return null;
    }
}