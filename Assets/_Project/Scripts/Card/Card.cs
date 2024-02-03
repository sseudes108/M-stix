using Mistix.Enums;
using UnityEngine;

namespace Mistix{
    public class Card: MonoBehaviour{
        private readonly ECardType _cardType;
        private readonly string _cardInfo;
        private bool _canMove;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        private void Update() {
            if(_canMove){
                Move();
            }
        }

        private void Move(){
            float moveSpeed = 5.0f;
            float rotationSpeed = 300.0f;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, _targetPosition) < 0.01f){
                _canMove = false;
            }
        }

        public virtual void SetUpCardData(ScriptableObject CardData){}

        public virtual ECardType GetCardType(){return _cardType;}

        public virtual string GetCardInfo(){return _cardInfo;}

        public void MoveCard(Vector3 targetPosition, Quaternion targetRotation){
            _canMove = true;
            _targetPosition = targetPosition;
            _targetRotation = targetRotation;
        }

        // protected virtual void OnMouseDown() {}
    }
}