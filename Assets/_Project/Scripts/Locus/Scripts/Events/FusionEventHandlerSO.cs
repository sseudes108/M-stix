using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FusionEventHandlerSO", menuName = "Mistix/Events/Fusion", order = 0)]
public class FusionEventHandlerSO : ScriptableObject {
    public UnityEvent OnFusionEnd;

    private void OnEnable() {
        OnFusionEnd ??= new UnityEvent();
    }

    public void FusionEnd() { OnFusionEnd?.Invoke(); }
}