using System;
using Mistix;
using UnityEngine;

public class CameraController : MonoBehaviour{

    public static CameraController Instance;

    public Transform PlayerCamera => _playerCamera;
    public Transform EnemyCamera => _enemyCamera;
    
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _enemyCamera;

    private Transform _targetPosition;
    private bool _canMove;

    private void Awake() {
        if(Instance != null){
            Errors.InstanceError(this);
        }
        Instance = this;
    }

    private void Update() {
        if(_canMove){
            float moveSpeed = 5f;
            float rotateSpeed = 300f;

            _mainCamera.position = Vector3.Lerp(_mainCamera.position, _targetPosition.position, moveSpeed * Time.deltaTime);

            _mainCamera.rotation = Quaternion.RotateTowards(_mainCamera.rotation,_targetPosition.rotation, rotateSpeed * Time.deltaTime);
        
            if(Vector3.Distance(_mainCamera.position, _targetPosition.position) < 0.02f & _mainCamera.rotation == _targetPosition.rotation){
                _canMove = false;
            }
        }
    }

    public void MoveCamera(Transform targetPosition){
        _targetPosition = targetPosition;
        _canMove = true;
    }
}