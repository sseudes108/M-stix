using UnityEngine;

public class PlayerHand : Hand {
    private Transform _OffCameraHand;
    private Vector3 _startPosition;
    private HandMovement _movement;

    private void Awake() {
        _movement = GetComponent<HandMovement>();
        _OffCameraHand = transform.Find("OffCam");
    }

    private void Start() {
        _startPosition = transform.position;
    }
    
    public override void OnEnable() {
        base.OnEnable();
        BattleManager.OnPlayerDraw.AddListener(BattleManager_OnPlayerDraw);
        BattleManager.OnCardSelectionEnd.AddListener(BattleManager_OnCardSelectionEnd);
    }

    public override void OnDisable() {
        base.OnDisable();
        BattleManager.OnPlayerDraw.AddListener(BattleManager_OnPlayerDraw);
        BattleManager.OnCardSelectionEnd.AddListener(BattleManager_OnCardSelectionEnd);
    }


    private void BattleManager_OnPlayerDraw() { Draw(); }

    private void BattleManager_OnCardSelectionEnd(){
        MoveHandOffScreen();
    }

    private void MoveHandOffScreen(){
        _movement.SetTargetPosition(_OffCameraHand.position);
    }

    private void MoveHandOnScreen(){
        _movement.SetTargetPosition(_startPosition);
    }
}