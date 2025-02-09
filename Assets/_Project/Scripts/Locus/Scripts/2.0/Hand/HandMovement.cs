using UnityEngine;

public class HandMovement : MonoBehaviour {
    // public Transform _handTransform;
    // private Vector3 _targetPosition;
    // public Vector3 StartPosition;

    // private bool _move = false;
    // private readonly float _moveSpeed = 3f;

    // private void Awake() {
    //     _handTransform = transform.Find("Hand");
    //     StartPosition = _handTransform.position;
    // }

    // private void FixedUpdate() {
    //     if(!_move) { return; }
    //     Move();
    // }

    // private void Move(){
    //     _handTransform.position = Vector3.Lerp(_handTransform.position, _targetPosition, _moveSpeed * Time.fixedDeltaTime);

    //     if(Vector3.Distance(_handTransform.position, _targetPosition) < 0.01f){
    //         _move = false;
    //     }
    // }

    // public void SetTargetPosition(Vector3 targetPosition){
    //     _move = true;
    //     _targetPosition = targetPosition;
    // }
}