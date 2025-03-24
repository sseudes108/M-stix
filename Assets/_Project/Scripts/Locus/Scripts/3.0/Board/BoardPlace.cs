using System.Collections.Generic;
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
        private bool _isOptShowing;
        private bool _isHighlighting;

    #region Unity
        private void Awake() {
            _colliders = GetComponents<Collider>();
            _visual = GetComponent<BoardPlaceVisual>();
        }

        private void Start(){
            IsFree = true;
            _boardManager = BoardManager.Instance;
        }

        private void OnMouseOver() {// Mostrar botoes de opção
            if(_cardInPlace == null) { return; }
            _cardInPlace.OnMouseOver(); //Atualiza a ilustração do UI

            if(_boardManager.IsActionPhase() == false){ return; }
            if(_isOptShowing){ return; }

            _boardManager.ShowOptions(_cardInPlace, this);
            _isOptShowing = true;
        }

        private void OnMouseExit(){
            _boardManager.HideOptions();
            if(_isOptShowing == false){ return; }
            _isOptShowing = false;
        }

        private void OnMouseDown(){
            if(_boardManager.IsBoardPlaceSelectionPhase()){

                if(!IsFree){// Place Ocupado
                    StartBoardFusion();
                    return;
                } 

                if(!_boardManager.IsPlayerTurn()){return;} // Foi clicado fora do turno correto

                SetCardInPlace(_boardManager.GetResultCard());
                return;
            }
        }
        
    #endregion

    #region Light
        public void LightUp(Color color){ _visual.LightUp(color); }
        public void LightOff(Color color){ _visual.LightOff(color); }
        public void HighLight(){
            _visual.HighLight(); 
            _isHighlighting = true;
        }

        public void UnHighLight(){
            Color color = new();
            
            if(_boardManager.IsPlayerTurn()){
                color = _boardManager.PlayerDefaultColor;
            }else{
                color = _boardManager.EnemyDefaultColor;
            }

            _visual.UnHighLight(color);
            _isHighlighting = false;
        }

    #endregion

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

        public void FlipCard(){
            if(_cardInPlace is MonsterCard){
                var monsterCard = _cardInPlace as MonsterCard;

                if(!monsterCard.IsFaceDown) { return; } // is face Up

                Quaternion rotation;

                if(monsterCard.IsPlayerCard){
                    //Monster in attack
                    if(monsterCard.IsInAttackMode){
                        monsterCard.MoveCard(transform);
                    }else{
                        //Monster in Defense
                        rotation = _boardManager.PlayerMonsterFaceUpDefRotation;
                        monsterCard.MoveCard(transform, rotation);
                    }

                }else{
                    //Monster in attack
                    if(monsterCard.IsInAttackMode){
                        monsterCard.MoveCard(transform);
                    }else{
                        //Monster in Defense
                        rotation = _boardManager.EnemyMonsterFaceUpDefRotation;
                        monsterCard.MoveCard(transform, rotation);
                    }

                }

                // _cardInPlace.SetWasFlipedThisTurn(true);
                _cardInPlace.SetCanFlip(false);
                _cardInPlace.SetFaceUp();
                return;
            }else{

            }
        }
        public Card GetCardInPlace(){return _cardInPlace; }
        public void ChangeMonsterToAtk(){
            var monsterCard = _cardInPlace as MonsterCard;
            Quaternion rotation;
            if(monsterCard.IsPlayerCard){
                if(monsterCard.IsFaceDown){
                    // rotation = PlayerMonsterFaceDownAtkRotation;
                    rotation = _boardManager.PlayerMonsterFaceDownAtkRotation;
                    monsterCard.MoveCard(transform, rotation);
                    return;
                }
                // rotation = _uiManager.PlayerMonsterFaceUpRotation();
                monsterCard.MoveCard(transform);
                monsterCard.SetAttackMode();
                monsterCard.SetCanChangeMode(false);
                return;
            }
        }

        public void ChangeMonsterToDef(){
            var monsterCard = _cardInPlace as MonsterCard;
            Quaternion rotation;
            if(monsterCard.IsPlayerCard){
                if(monsterCard.IsFaceDown){         
                    rotation = _boardManager.PlayerMonsterFaceDownDefRotation;
                    monsterCard.MoveCard(transform, rotation);
                    return;
                }
                rotation = _boardManager.PlayerMonsterFaceUpDefRotation;
                monsterCard.MoveCard(transform, rotation);
                monsterCard.SetDeffenseMode();
                monsterCard.SetCanChangeMode(false);
                return;
            }
        }

        public bool IsPlaceHighlighting(){ return _isHighlighting; }

        private void StartBoardFusion(){
            var newCardList = new List<Card>{_cardInPlace, _boardManager.GetResultCard()};
            // _cardManager.Selector.SetCardsToBoardFusion(newCardList);
            // _battleManager.Battle.ChangeState(_battleManager.Battle.Fusion);//Change phase back to fusion
            _boardManager.SetCardsToBoardFusion(newCardList);
            // _boardManager.SetBoardFusion();
            _boardManager.ChangeToFusionPhase();//Change phase back to fusion
            SetPlaceFree();
        }

        public void SetPlaceFree(){
            _cardInPlace = null;
            // _canBeSelected = true;
            IsFree = true;
        }
    }
}