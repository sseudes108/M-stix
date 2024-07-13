using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AIActor", menuName = "Mistix/AI/Actor", order = 0)]
public class AIActorSO : ScriptableObject {
    [HideInInspector] public UnityEvent OnSelectionFinished;

    public AICardSelectorSO CardSelector { get; private set; }
    
    private void OnEnable() {
        Debug.Log("AIActorSO OnEnable()");
        CardSelector ??= new(this);

        OnSelectionFinished ??= new UnityEvent();
    }

    public void CardSelectionFinished(){
        Debug.Log("CardSelectionFinished()");
        OnSelectionFinished?.Invoke();
    }
}