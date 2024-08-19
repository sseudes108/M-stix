using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// AIActorSO Is used to manage all the Ai Actions during the battle. All the events of the actions should be invocked from here, since the actions are not monobehaviours
/// </summary>

[CreateAssetMenu(fileName = "AIActor", menuName = "Mistix/AI/Actor", order = 0)]
public class AIActorSO : ScriptableObject {
    //Actions
    public AICardSelector CardSelector { get; private set; }
    public AICardStatSelector CardStatSelector { get; private set; }
    public AIBoardPlaceSelector BoardPlaceSelector { get; private set; }

    //Events
    [HideInInspector] public UnityEvent CardSelector_OnSelectionFinished;
    [HideInInspector] public UnityEvent CardStatSelector_OnCardStatSelectionFinished;
    [HideInInspector] public UnityEvent BoardPlaceSelector_OnBoardPlaceSelected;
    
    private void OnEnable() {
        CardSelector ??= new(this);
        CardStatSelector ??= new(this);
        BoardPlaceSelector ??= new(this);

        CardSelector_OnSelectionFinished ??= new UnityEvent();
        CardStatSelector_OnCardStatSelectionFinished ??= new UnityEvent();
    }

    public void CardSelectionFinished(){
        CardSelector_OnSelectionFinished?.Invoke();
    }

    public void CardStatSelectionFinished(){
        CardStatSelector_OnCardStatSelectionFinished?.Invoke();
    }

    public void BoardPlaceSelected(){
        BoardPlaceSelector_OnBoardPlaceSelected?.Invoke();
    }

    public void SetBoardPlaces(List<BoardPlace> monsterPlaces, List<BoardPlace> arcanePlaces){
        BoardPlaceSelector.SetBoardPlaces(monsterPlaces, arcanePlaces);
    }
}