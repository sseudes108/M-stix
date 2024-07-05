using UnityEngine;

public class PlayerHand : Hand {
[SerializeField] private Transform _OffCameraHand;

#region Unity Methods

    public override void OnEnable() {
        base.OnEnable();
        DrawPhase.OnPlayerDraw += DrawPhase_OnPlayerDraw;
        UIBattleScene.OnSelectionFinished += UIBattleScene_OnSelectionFinished;
    }

    public override void OnDisable() {
        base.OnDisable();
        DrawPhase.OnPlayerDraw -= DrawPhase_OnPlayerDraw;
        UIBattleScene.OnSelectionFinished += UIBattleScene_OnSelectionFinished;
    }

    public override void Awake() {
        base.Awake();
        _OffCameraHand = transform.Find("OffCam");
    }
// 37338
#endregion

#region Events Methods

    public override void StartPhase_OnStartPhase(){
        base.StartPhase_OnStartPhase();
        _movement.SetTargetPosition(_movement.StartPosition);
    }

    private void UIBattleScene_OnSelectionFinished(){
        if(this != null){
            MoveCameraOffView();
        }else{
            Destroy(this);
        }
    }

    private void MoveCameraOffView(){
        _movement.SetTargetPosition(_OffCameraHand.position);
    }

    private void DrawPhase_OnPlayerDraw(){
        Draw();
    }

#endregion
    
}