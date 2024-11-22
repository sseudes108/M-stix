using System.Collections;
using UnityEngine;

public class AIEffectSelector : AIAction {
    public AIEffectSelector(AIActorSO actor) { _actor = actor; }

    public IEnumerator SelectEffectRoutine(){
        Debug.Log("SelectEffectRoutine()");
        _actor.EffectSelected();
        yield return null;
    }
}