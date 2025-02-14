using UnityEngine;

namespace Mistix{

    [RequireComponent(typeof(BoardPlaceVisual))]
    public class BoardPlace : MonoBehaviour {

        [field:SerializeField] public EBoardPlace Location { get; private set; }
        [field:SerializeField] public bool IsPlayerPlace { get; private set; }
        [field:SerializeField] public bool IsMonsterPlace { get; private set; }

        private BoardManager _boardManager;
        private Collider[] _colliders;
        public bool IsFree { get; private set; }

        private BoardPlaceVisual _visual;
        private Card _cardInPlace;

        private void Awake() {
            _colliders = GetComponents<Collider>();
            _visual = GetComponent<BoardPlaceVisual>();
        }

        private void Start(){
            IsFree = true;
            _boardManager = BoardManager.Instance;
        }

        public void LightUp(Color color){
            _visual.LightUp(color);
        }

        public void LightOff(Color color){
            _visual.LightOff(color);
        }

        public void HighLight(){
            _visual.HighLight();
        }

        // private void OnMouseOver() {// Mostrar botoes de opção
        //     if(_cardInPlace == null) { return; }
        //     _cardInPlace.OnMouseOver(); //Atualiza a ilustração do UI
        // }

        private void OnMouseDown(){
            if(_boardManager.IsBoardPlaceSelectionPhase()){
                if(!IsFree){return;}

                if(!_boardManager.IsPlayerTurn()){return;}
                SetCardInPlace(_boardManager.GetResultCard());
                return;
            }
        }

        public void SetCardInPlace(Card card){
            if(card is MonsterCard){
                var monsterCard = card as MonsterCard;
                if(monsterCard.IsInAttackMode){//In attacK

                    if(monsterCard.IsFaceDown){// In Attack Face Down

                        Quaternion rotation;

                        if(monsterCard.IsPlayerCard){
                            rotation = _boardManager.PlayerMonsterFaceDownAtkRotation;
                        }else{
                            rotation = _boardManager.EnemyMonsterFaceDownAtkRotation;
                        }

                        card.MoveCard(transform, rotation);

                    }else{ // In Attack Face Up
                        card.MoveCard(transform);
                    }
                    
                }else{ // In Deffense
                    if(monsterCard.IsFaceDown){// In Deffense Face Down

                        Quaternion rotation;

                        if(monsterCard.IsPlayerCard){
                            rotation = _boardManager.PlayerMonsterFaceDownDefRotation;
                        }else{
                            rotation = _boardManager.EnemyMonsterFaceDownDefRotation;
                        }

                        card.MoveCard(transform, rotation);

                    }else{ // In Deffense Face Up

                        Quaternion rotation;

                        if(monsterCard.IsPlayerCard){
                            rotation = _boardManager.PlayerMonsterFaceUpDefRotation;
                        }else{
                            rotation = _boardManager.EnemyMonsterFaceUpDefRotation;
                        }

                        card.MoveCard(transform, rotation);
                    }
                }
            }else{// Arcane Card

            }

            _cardInPlace = card;
            card.SetBoardPlace(this);
            // card.DeselectCard();
            card.DisableCollider();
            
            IsFree = false;
            // _canBeSelected = false;
            _boardManager.BoardPlaceSelected();
        }
    }
}