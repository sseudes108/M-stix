using UnityEngine;

namespace Mistix{
    public class HandMovement : MonoBehaviour {
        [SerializeField] private Transform _handTransform;
        [SerializeField] private Transform _OffCameraHand;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;

        private bool _move = false;
        private readonly float _moveSpeed = 3f;

        private void Awake() {
            _startPosition = _handTransform.position;
        }

        private void FixedUpdate() {
            if(!_move) { return; }
            Move();
        }

        private void Move(){
            _handTransform.position = Vector3.Lerp(_handTransform.position, _targetPosition, _moveSpeed * Time.fixedDeltaTime);

            if(Vector3.Distance(_handTransform.position, _targetPosition) < 0.01f){
                _move = false;
            }
        }

        public void SetTargetPosition(Vector3 targetPosition){
            _move = true;
            _targetPosition = targetPosition;
        }

        public void MoveHandOffScreen(){
            SetTargetPosition(_OffCameraHand.transform.position);
        }

        public void MoveHandOnScreen(){
            SetTargetPosition(_startPosition);
        }
    }
}