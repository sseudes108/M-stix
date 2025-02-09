using UnityEngine;

namespace Mistix{
    public class CardMovement : MonoBehaviour {
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        public (Vector3, Quaternion) GetStartPositionAndRotation() => (_startPosition, _startRotation);

        private bool _moveCard = false;
        private float _moveSpeed = 5.0f;

        private void Start() {
            transform.GetPositionAndRotation(out _startPosition, out _startRotation);
        }

        private void FixedUpdate() {
            if(!_moveCard) { return; }
            Move();
        }

        private void Move(){
            float rotateSpeed = 400.0f;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotateSpeed * Time.fixedDeltaTime);

            if(Vector3.Distance(transform.position, _targetPosition) < 0.01f && transform.rotation == _targetRotation){
                _moveCard = false;
            }
        }

        public void SetTargetPosition(Vector3 targetPosition, Quaternion targetRotation, float speed){
            _moveSpeed = speed;
            _targetPosition = targetPosition;
            _targetRotation = targetRotation;
        }

        public void SetTargetPosition(Vector3 targetPosition, float speed){
            _moveSpeed = speed;
            _targetPosition = targetPosition;
        }

        public void SetTargetRotation(Quaternion targetRotation) { _targetRotation = targetRotation; }

        public void AllowMovement(bool allow) { _moveCard = allow; }
    }
}