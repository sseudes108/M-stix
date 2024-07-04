using System.Collections;
using UnityEngine;

public class PlayerHand : Hand {
[SerializeField] private Transform _OffCameraHand;

#region Unity Methods

    public override void OnEnable() {
        // Debug.Log("OnEnable() from player Hand Called");
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
        // Debug.Log($"Player Hand Instance ID <color=red>{this.GetInstanceID()}</color=red>");
        // Debug.Log("Awake from player Hand Called");
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
        // Debug.Log("Player Hand - UIBattleScene_OnSelectionFinished");
        if(this != null){
            _movement.SetTargetPosition(_OffCameraHand.position);
        }else{
            // Debug.Log($"Destroy Player Hand Instance ID <color=yellow>{this.GetInstanceID()}</color=yellow>");
            Destroy(this);
        }
    }

    private void DrawPhase_OnPlayerDraw(){
        Draw();
    }

#endregion
    
}