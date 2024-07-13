using UnityEngine;

public class PlayerHand : Hand {
[SerializeField] private Transform _OffCameraHand;
[SerializeField] private UIEventHandlerSO UIManager;
[SerializeField] private TurnManagerSO _turnManager;


#region Unity Methods

    public override void OnEnable() {
        _battleManager.OnPlayerDraw.AddListener(BattleManager_OnPlayerDraw);
        UIManager.OnCardSelectionFinished.AddListener(UIManager_OnCardSelectionFinished);
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    public override void OnDisable() {
        _battleManager.OnPlayerDraw.RemoveListener(BattleManager_OnPlayerDraw);
        UIManager.OnCardSelectionFinished.RemoveListener(UIManager_OnCardSelectionFinished);
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    public override void Awake() {
        base.Awake();
        _OffCameraHand = transform.Find("OffCam");
    }

// 37338
#endregion

#region Events Methods

    public override void BattleManager_OnStartPhase(){
        base.BattleManager_OnStartPhase();
        if(_turnManager.IsPlayerTurn()){
            _movement.SetTargetPosition(_movement.StartPosition);
        }
    }

    private void UIManager_OnCardSelectionFinished(){
        if(this != null){
            MoveCameraOffView();
        }
    }

    private void BattleManager_OnPlayerDraw(){
        _handManager.Draw(this);
    }

    private void MoveCameraOffView(){
        _movement.SetTargetPosition(_OffCameraHand.position);
    }

#endregion
    
}