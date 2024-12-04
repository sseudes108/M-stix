using System.Collections;
using UnityEngine;

public class AIAttackSelector : AIAction {
    public AIAttackSelector(AI ai, AIActor actor) {
        _AI = ai;
        _Actor = actor;
    }

    public IEnumerator SelectAttackRoutine(){
        Debug.Log("SelectAttackRoutine()");
        Debug.LogWarning("Attacking!!!!");
        yield return new WaitForSeconds(2.5f);
        _Actor.AttackingMonster.SetCanAttack(false);
        _Actor.ResetAttackPoints();
        _AI.ChangeState(_AI.ActionSelect);
        yield return null;
    }
}