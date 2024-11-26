using System.Collections;
using UnityEngine;

public class AISelectCardState : AbstractState{
    public AISelectCardState(StateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() { StateMachine.StartCoroutine(AIRoutine()); }

    public override void Exit(){}

    public IEnumerator AIRoutine(){
        yield return new WaitForSeconds(0.5f);
        yield return StateMachine.StartCoroutine(StateMachine.AI.Actor.CardSelector.SelectCardRoutine());
        yield return null;
    }

    public override string ToString(){
        return "Select Card";
    }
}

// public class AISelectCardState : AbstractState{
//     public AISelectCardState(StateMachine controller) : base(controller){}

//     private AI _AI;
//     // private AIActorSO _actor;

//     public override void Enter(){
//         _AI = StateMachine.AI;
//         // _actor = _AI.Actor;
        
//         _AI.StartCoroutine(AIRoutine());
//     }

//     public override void Exit(){}

//     public IEnumerator AIRoutine(){
//         yield return new WaitForSeconds(0.5f);
//         yield return _AI.StartCoroutine(_AI.Actor.CardSelector);
//         // yield return _AI.StartCoroutine(_actor.CardSelector.SelectCardRoutine());
//         yield return null;
//     }

//     public override string ToString(){
//         return "Select Card";
//     }
// }