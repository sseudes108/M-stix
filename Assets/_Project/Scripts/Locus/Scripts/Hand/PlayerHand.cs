using UnityEngine;

public class PlayerHand : Hand {
    private Transform _OffCameraHand;
    private Transform _OnCameraHand;
    private HandMovement _movement;
    [SerializeField] private TurnManagerSO _turnManager;

    private void Awake() {
        _movement = GetComponent<HandMovement>();
        _OffCameraHand = transform.Find("OffCam");
        _OnCameraHand = transform.Find("OnCam");
    }

    public override void OnEnable() {
        base.OnEnable();
        BattleManager.OnPlayerDraw.AddListener(BattleManager_OnPlayerDraw);
        BattleManager.OnCardSelectionEnd.AddListener(BattleManager_OnCardSelectionEnd);
        BattleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    public override void OnDisable() {
        base.OnDisable();
        BattleManager.OnPlayerDraw.RemoveListener(BattleManager_OnPlayerDraw);
        BattleManager.OnCardSelectionEnd.RemoveListener(BattleManager_OnCardSelectionEnd);
        BattleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    private void BattleManager_OnStartPhase(){
        if(_turnManager.CurrentTurn != 1 && _turnManager.IsPlayerTurn){
            MoveHandOnScreen();
        }
    }

    private void BattleManager_OnPlayerDraw() { Draw(); }

    private void BattleManager_OnCardSelectionEnd(){
        MoveHandOffScreen();
    }

    private void MoveHandOffScreen(){
        _movement.SetTargetPosition(_OffCameraHand.position);
    }

    private void MoveHandOnScreen(){
        _movement.SetTargetPosition(_OnCameraHand.position);
    }
}