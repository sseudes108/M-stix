using UnityEngine;

public class Movement : MonoBehaviour {
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _canMove;
    private float _moveSpeed = 5.0f;

    private void Start() {
        transform.GetPositionAndRotation(out _startPosition, out _startRotation);
    }

    private void Update() {
        if(_canMove){
            Move();
        }
    }

    private void Move(){
        float rotateSpeed = 300.0f;
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotateSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _targetPosition) < 0.2f && transform.rotation == _targetRotation){
            _canMove = false;
        }
    }

    public void SetTargetPosition(Vector3 targetPosition, Quaternion targetRotation, float speed){
        _moveSpeed = speed;
        _targetPosition = targetPosition;
        _targetRotation = targetRotation;

        _canMove = true;
    }

    public void SetTargetPosition(Vector3 targetPosition, float speed){
        _moveSpeed = speed;
        _targetPosition = targetPosition;

        _canMove = true;
    }

    public (Vector3,Quaternion) GetStartPositionAndRotation() => (_startPosition, _startRotation);
}