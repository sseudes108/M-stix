using UnityEngine;

public class PlayerHand : Hand {
[SerializeField] private Transform _OffCameraHand;
[SerializeField] private UIEventHandlerSO UIManager;

#region Unity Methods

    public override void OnEnable() {
        base.OnEnable();
        BattleManager.OnPlayerDraw.AddListener(BattleManager_OnPlayerDraw);
        UIManager.OnCardSelectionFinished.AddListener(UIManager_OnCardSelectionFinished);
    }

    public override void OnDisable() {
        base.OnDisable();
        BattleManager.OnPlayerDraw.RemoveListener(BattleManager_OnPlayerDraw);
        UIManager.OnCardSelectionFinished.RemoveListener(UIManager_OnCardSelectionFinished);
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
        _movement.SetTargetPosition(_movement.StartPosition);
    }

    private void UIManager_OnCardSelectionFinished(){
        if(this != null){
            MoveCameraOffView();
        }else{
            Destroy(this);
        }
    }

    private void MoveCameraOffView(){
        _movement.SetTargetPosition(_OffCameraHand.position);
    }

    private void BattleManager_OnPlayerDraw(){
        Draw();
    }

#endregion
    
}