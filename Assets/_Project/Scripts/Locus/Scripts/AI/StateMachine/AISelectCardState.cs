using System.Collections;
using UnityEngine;

public class AISelectCardState : AbstractState{
    public AISelectCardState(StateMachine controller) : base(controller){}

    private AI _AI;
    private AIActorSO _actor;

    public override void Enter(){
        _AI = StateMachine.AI;
        _actor = _AI.Actor;
        
        _actor.SplitCardsOnBoardByType(); //Verifica quais as cartas em campo
        _AI.StartCoroutine(AIRoutine());
    }

    public override void Exit(){}

    public IEnumerator AIRoutine(){
        yield return new WaitForSeconds(0.5f);
        _actor.UpdateCardLists(_AI.Manager.CardsInHand, _actor.CardsOnField.MonstersOnAIField);
        yield return _AI.StartCoroutine(_actor.CardSelector.SelectCardRoutine());
        yield return null;
    }

    public override string ToString(){
        return "Select Card";
    }
}