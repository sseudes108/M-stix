using Mistix;
using UnityEngine;

public class FusionStageControllerPosition : MonoBehaviour{

    [SerializeField] private Transform _playerHand, _enemyHand;
    private Vector3 _playerHandStartPosition, _enemyHandStartPosition;

    private void OnEnable() {
        BattleManager.Instance.Fusion.OnFusionStarted += Fusion_OnFusionStarted;
        BattleManager.Instance.Fusion.OnFusionEnded += Fusion_OnFusionEnded;
    }

    private void OnDisable() {
        BattleManager.Instance.Fusion.OnFusionStarted -= Fusion_OnFusionStarted;
        BattleManager.Instance.Fusion.OnFusionEnded -= Fusion_OnFusionEnded;
    }

    private void Start() {
        _playerHandStartPosition = _playerHand.position;
        _enemyHandStartPosition = _enemyHand.position;
    }

    private void Fusion_OnFusionStarted(){
        Debug.Log("Fusion_OnFusionStarted Invoked");

        if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
            Vector3 targetPosition = new(0.72f,-1f,-3f);
            _playerHand.GetComponent<Hand>().MoveHand(targetPosition);

        }else{
            Vector3 targetPosition = new(0.72f,-1f,-3f);
            _enemyHand.GetComponent<Hand>().MoveHand(targetPosition);
        }
    }
    
    private void Fusion_OnFusionEnded(){
        Debug.Log("Fusion_OnFusionEnded Invoked");

        if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
            Vector3 targetPosition = _playerHandStartPosition;
            _playerHand.GetComponent<Hand>().MoveHand(targetPosition);

        }else{
            Vector3 targetPosition = _enemyHandStartPosition;
            _enemyHand.GetComponent<Hand>().MoveHand(targetPosition);
        }
    }
}
