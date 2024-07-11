using System.Collections;
using UnityEngine;

public class StartPhase : AbstractState{
    public override void Enter(){
        if(Battle != null){
            Battle.StartCoroutine(BattlePhaseStartRoutine());
        }
    }

    public override void Exit(){}
    
    public IEnumerator BattlePhaseStartRoutine(){
        yield return new WaitForSeconds(1f);
        Battle.BattleManager.StartPhase();

        yield return new WaitForSeconds(2f);
        Battle.ChangeState(Battle.DrawPhase);

        yield return null;
    }

    public override string ToString(){ return "Start"; }
}