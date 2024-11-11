using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UIEventHandlerSO", menuName = "Mistix/Events/UI", order = 0)]
public class UIEventHandlerSO : ScriptableObject {
    public UnityEvent  OnCardSelectionFinished;
    public UnityEvent<Texture2D> OnUpdateIllustration;

    private void OnEnable() {
        OnCardSelectionFinished ??= new UnityEvent();
    }

    public void CardSelectionFinished() { OnCardSelectionFinished?.Invoke(); }
    public void UpdateIllustration(Texture2D newIllustration) { OnUpdateIllustration?.Invoke(newIllustration); }
}