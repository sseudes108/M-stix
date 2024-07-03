using System;
using System.Collections;
using UnityEngine;

public class StartPhase : AbstractState{
    public static Action OnStartPhase;

    public override void Enter(){
        Battle.StartCoroutine(BattlePhaseStartRoutine());
    }

    public override void Exit(){}
    public override void LogicUpdate(){}

    public IEnumerator BattlePhaseStartRoutine(){
        OnStartPhase?.Invoke();
        yield return new WaitForSeconds(3f);
        Battle.ChangeState(Battle.DrawPhase);
        yield return null;
    }

    public override string ToString(){ return "Start"; }
}