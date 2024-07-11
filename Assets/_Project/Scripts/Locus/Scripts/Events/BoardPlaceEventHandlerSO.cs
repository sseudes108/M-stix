using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoardPlaceEventHandlerSO", menuName = "Mistix/Events/BoardPlace", order = 0)]
public class BoardPlaceEventHandlerSO : ScriptableObject {
    public UnityEvent OnBoardPlaceSelected;
    public UnityEvent<BoardPlace> OnShowOptions;
    public UnityEvent OnHideOptions;
    
    private void OnEnable() {
        OnBoardPlaceSelected ??= new UnityEvent();
        OnShowOptions ??= new UnityEvent<BoardPlace>();
        OnHideOptions ??= new UnityEvent();
    }

    public void BoardPlaceSelected() { OnBoardPlaceSelected?.Invoke(); }
    public void ShowOptions(BoardPlace place) { OnShowOptions?.Invoke(place); }
    public void HideOptions() { OnHideOptions?.Invoke(); }
}