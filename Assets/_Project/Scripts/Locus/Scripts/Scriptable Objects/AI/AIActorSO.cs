using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// AIActorSO Is used to manage all the Ai Actions during the battle. All the events of the actions should be invocked from here, since the actions are not monobehaviours
/// </summary>

[CreateAssetMenu(fileName = "AIActor", menuName = "Mistix/AI/Actor", order = 0)]
public class AIActorSO : ScriptableObject {
    //Actions
    public AICardSelector CardSelector { get; private set; }

    //Events
    [HideInInspector] public UnityEvent CardSelector_OnSelectionFinished;
    
    private void OnEnable() {
        CardSelector ??= new(this);

        CardSelector_OnSelectionFinished ??= new UnityEvent();
    }

    public void CardSelectionFinished(){
        CardSelector_OnSelectionFinished?.Invoke();
    }

}