using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UIEventHandlerSO", menuName = "Mistix/Events/UI", order = 0)]
public class UIEventHandlerSO : ScriptableObject {
    public UnityEvent  OnCardSelectionFinished;
    public UnityEvent<bool, int>  OnDeckCountUpdate;
    public UnityEvent<bool, int>  OnLifePointsUpdate;
    public UnityEvent<Texture2D> OnUpdateIllustration;

    private void OnEnable() {
        OnCardSelectionFinished ??= new UnityEvent();
        OnDeckCountUpdate ??= new UnityEvent<bool, int>();
        OnLifePointsUpdate ??= new UnityEvent<bool, int>();
    }

    public void CardSelectionFinished() { OnCardSelectionFinished?.Invoke(); }
    public void UpdateIllustration(Texture2D newIllustration) { OnUpdateIllustration?.Invoke(newIllustration); }
    public void UpadateDeckCount(bool isPlayerDeck, int deckCount) { OnDeckCountUpdate?.Invoke(isPlayerDeck, deckCount); }
    public void UpdateLifePoints(bool isPlayerLP, int lifePoints) { OnLifePointsUpdate?.Invoke(isPlayerLP, lifePoints); }
}