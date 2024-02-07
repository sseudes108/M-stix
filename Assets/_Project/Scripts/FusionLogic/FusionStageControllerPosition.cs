using System;
using Mistix;
using Mistix.FusionLogic;
using UnityEngine;

public class FusionStageControllerPosition : MonoBehaviour{
    public static FusionStageControllerPosition Instance;

    [SerializeField] private Transform _playerHand, _enemyHand;
    private Vector3 _playerHandStartPosition, _enemyHandStartPosition;

    private void OnEnable() {
        Fusion.Instance.OnFusionStarted += Fusion_OnFusionStarted;
        Fusion.Instance.OnFusionEnded += Fusion_OnFusionEnded;
    }

    private void OnDisable() {
        Fusion.Instance.OnFusionStarted -= Fusion_OnFusionStarted;
        Fusion.Instance.OnFusionEnded -= Fusion_OnFusionEnded;
    }

    private void Awake() {
        if(Instance != null){
            Errors.InstanceError(this);
        }
        Instance = this;
    }

    private void Start() {
        _playerHandStartPosition = _playerHand.position;
        _enemyHandStartPosition = _enemyHand.position;
    }

    private void Fusion_OnFusionStarted(){
        Debug.Log("Fusion_OnFusionStarted Invoked");

        if(TurnSystem.Instance.IsPlayerTurn()){
            Vector3 targetPosition = new(0.72f,-1f,-3f);
            _playerHand.GetComponent<Hand>().MoveHand(targetPosition);

        }else{
            Vector3 targetPosition = new(0.72f,-1f,-3f);
            _enemyHand.GetComponent<Hand>().MoveHand(targetPosition);
        }
    }
    private void Fusion_OnFusionEnded(){
        Debug.Log("Fusion_OnFusionEnded Invoked");

        if(TurnSystem.Instance.IsPlayerTurn()){
            Vector3 targetPosition = _playerHandStartPosition;
            _playerHand.GetComponent<Hand>().MoveHand(targetPosition);

        }else{
            Vector3 targetPosition = _enemyHandStartPosition;
            _enemyHand.GetComponent<Hand>().MoveHand(targetPosition);
        }
    }

}
