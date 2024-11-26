using System.Collections;
public class AIEffectSelector : AIAction {
    public AIEffectSelector(AIActor actor) { _Actor = actor; }

    public IEnumerator SelectEffectRoutine(){
        _AI.Actor.EffectSelected();
        yield return null;
    }
}