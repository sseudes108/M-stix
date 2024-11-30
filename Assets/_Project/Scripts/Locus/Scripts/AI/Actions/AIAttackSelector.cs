using System.Collections;
using UnityEngine;

public class AIAttackSelector : AIAction {
    public AIAttackSelector(AIActor actor) {
        _Actor = actor;
    }

    public IEnumerator SelectAttackRoutine(){
        Debug.Log("SelectAttackRoutine()");
        _Actor.FieldChecker.AIMonstersOnFieldThatCanAttack[0].SetCanAttack(false);
        _AI.ChangeState(_AI.ActionSelect);

        yield return null;
    }
}