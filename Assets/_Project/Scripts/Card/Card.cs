using Mistix.Enums;
using UnityEngine;

namespace Mistix{
    public class Card: MonoBehaviour{
        private readonly ECardType _cardType;
        private readonly string _cardInfo;

        Vector3 _destination;
        Quaternion _rotation;

        protected void Update(){
            
            // Converte a posição local do destino para posição global
            Vector3 globalDestination = _destination;
            if (transform.parent != null) {
                globalDestination = transform.parent.TransformPoint(_destination);
            }

            if(Vector3.Distance(transform.position, globalDestination) <= 0.1f) return;

            if(Vector3.Distance(transform.position, globalDestination) > 0.1f) {
                // Move gradualmente para a posição de destino usando Lerp
                transform.position = Vector3.Lerp(transform.position, globalDestination, 2.0f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, 2.0f * Time.deltaTime);
            }
        }

        public virtual void SetUpCardData(ScriptableObject CardData){}


        public virtual ECardType GetCardType(){return _cardType;}

        public virtual string GetCardInfo(){return _cardInfo;}

        public void MoveCard(Vector3 destination, Quaternion rotation){
            _destination = destination;
            _rotation = rotation;
        }
    }
}