using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoardPlaceManager", menuName = "Mistix/Managers/BoardPlace", order = 0)]
public class BoardPlaceEventHandlerSO : ScriptableObject {   
    [HideInInspector] public UnityEvent OnBoardPlaceSelected;
    [HideInInspector] public UnityEvent<BoardPlace> OnShowOptions;
    [HideInInspector] public UnityEvent OnHideOptions;
    
    private void OnEnable() {
        OnBoardPlaceSelected ??= new UnityEvent();
        OnShowOptions ??= new UnityEvent<BoardPlace>();
        OnHideOptions ??= new UnityEvent();
    }

    // Events
    public void BoardPlaceSelected() { OnBoardPlaceSelected?.Invoke(); }
    public void ShowOptions(BoardPlace place) { OnShowOptions?.Invoke(place); }
    public void HideOptions() { OnHideOptions?.Invoke(); }

}