using UnityEngine;

public class UIBattleManager : MonoBehaviour {
    [SerializeField] private UICardPlaceHolder _UICardPlaceHolder;

    private void Awake() {
        _UICardPlaceHolder = GetComponentInChildren<UICardPlaceHolder>();
    }

    public UICardPlaceHolder UICardPlaceHolder => _UICardPlaceHolder;
}