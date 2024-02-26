using Mistix.Enums;
using UnityEngine;

namespace Mistix{
    public class Card: MonoBehaviour{
        protected ECardType _cardType;
        private readonly string _cardInfo;
        [SerializeField] protected Texture2D _ilustration;

        //Move
        private bool _canMove;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        //Select
        private bool _selected = false;

        private void Update() {
            if(_canMove){
                GetComponent<Collider>().enabled = false;
                Move();
            }
        }

        private void Move(){
            float moveSpeed = 5.0f;
            float rotationSpeed = 300.0f;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, _targetPosition) < 0.02f & transform.rotation == _targetRotation){

                //** Dont enable collider if the card is in the fusion line **//
                var cardInFusionLine = GetComponentInParent<FusionPosition>();
                if(cardInFusionLine == null){
                    GetComponent<Collider>().enabled = true;
                }

                _canMove = false;
            }
        }

        public virtual void SetUpCardData(ScriptableObject CardData){}

        public ECardType GetCardType(){
            var isMonster = GetComponent<MonsterCard>();

            if(isMonster != null){
                _cardType = ECardType.Monster;
            }else{
                _cardType = ECardType.Arcane;
            }

            return _cardType;
        }

        public virtual string GetCardInfo(){return _cardInfo;}

        public virtual Texture2D GetCardIlustration(){return _ilustration;}
        public void MoveCard(Vector3 targetPosition, Quaternion targetRotation){
            _canMove = true;
            _targetPosition = targetPosition;
            _targetRotation = targetRotation;
        }

        private void OnMouseDown(){
            if(!_selected){
                SelectCard();
            }else{
                DeselectCard();
            }
            GetCardInfo();
        }

        private void SelectCard(){
            var newPosition = new Vector3();

            if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
                newPosition = new Vector3(0f, 0.3f, 0.3f);
            }else{
                newPosition = new Vector3(0f, 0.3f, -0.3f);
            }

            transform.position += newPosition;
            _selected = true;

            BattleManager.Instance.CardSelector.AddCardToSelectedList(this);
        }

        private void DeselectCard(){
            var newPosition = new Vector3();

            if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
                newPosition = new Vector3(0f, -0.3f, -0.3f);
            }else{
                newPosition = new Vector3(0f, -0.3f, 0.3f);
            }
            
            transform.position += newPosition;
            _selected = false;

            BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(this);
        }
    }
}