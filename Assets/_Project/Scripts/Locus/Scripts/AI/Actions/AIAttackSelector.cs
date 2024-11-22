using System.Collections;
using UnityEngine;

public class AIAttackSelector : AIAction {
    public AIAttackSelector(AIActorSO actor) { _actor = actor; }

    public IEnumerator SelectAttackRoutine(){
        Debug.Log("SelectAttackRoutine()");
        yield return null;
        _actor.AIManager.AI.ChangeState(_actor.AIManager.AI.ActionSelect);
    }
}