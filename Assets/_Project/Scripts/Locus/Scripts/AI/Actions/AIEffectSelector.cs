using System.Collections;
using UnityEngine;

public class AIEffectSelector : AIAction {
    public AIEffectSelector(AIActor actor) { _Actor = actor; }

    public IEnumerator SelectEffectRoutine(){
        Debug.Log("AIEffectSelector.cs - SelectEffectRoutine()");
        //Implemente Arcanes on field check

        _Actor.EffectSelected(); //Effect selection Finished
        yield return null;
    }
}