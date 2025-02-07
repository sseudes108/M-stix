using System;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(BoardPlaceVisual))]
public class BoardPlace : MonoBehaviour {
    
//     [SerializeField] private BattleManagerSO _battleManager;
//     [SerializeField] private BoardManagerSO _boardManager;
//     [SerializeField] private CardManagerSO _cardManager;
//     [SerializeField] private TurnManagerSO _turnManager;

//     [field:SerializeField] public EBoardPlace Location { get; private set; }
//     [field:SerializeField] public Collider[] Colliders { get; private set; }
//     [field:SerializeField] public bool IsPlayerPlace { get; private set; }
//     [field:SerializeField] public bool IsMonsterPlace { get; private set; }
//     [field:SerializeField] public bool IsFree { get; private set; }

//     private Card _resultCard;
//     public Card CardInPlace;

//     private bool _canBeSelected;
//     private bool _isOptShowing;

//     public BoardPlaceVisual Visual;

//     private void OnEnable() {
//         _battleManager.OnBoardPlaceSelectionStart.AddListener(BattleManager_OnBoardPlaceSelectionStart);
//         _boardManager.OnBoardPlaceSelected.AddListener(BoardManager_OnBoardPlaceSelected);
//     }
    
//     private void OnDisable() {
//         _battleManager.OnBoardPlaceSelectionStart.RemoveListener(BattleManager_OnBoardPlaceSelectionStart);
//         _boardManager.OnBoardPlaceSelected.RemoveListener(BoardManager_OnBoardPlaceSelected);
//     }

// #region Unity Methods

//     private void Awake() {
//         Colliders = GetComponents<Collider>();
//         Visual = GetComponent<BoardPlaceVisual>();
//     }

//     private void Start(){
//         IsFree = true;
//     }

//     private void OnMouseOver(){
//         if(this == null) { return; }

//         if(CardInPlace == null) { return; }
//         CardInPlace.OnMouseOver(); //Update the illustration in UICard

//         if(_battleManager.CurrentPhase != _battleManager.Battle.Action) { return; }
//         if(_isOptShowing) { return; }

//         _boardManager.ShowOptions(this);
//         _isOptShowing = true;
//     }

//     private void OnMouseExit(){
//         _boardManager.HideOptions();
//         _isOptShowing = false;
//     }

//     private void OnMouseDown() {
//         switch (_battleManager.CurrentPhase){
//             case BoardPlaceSelectionPhase:
//                 if(!_canBeSelected) { return; }

//                 if(IsFree){
//                     if(!_turnManager.IsPlayerTurn) { return; }
//                     SetCardInPlace(_resultCard);
//                     return;
//                 }
                
//                 //Board Fusion
//                 StartBoardFusion();
//             break;

//             case AttackSelectionPhase:
//                 if(!IsFree){
//                     _battleManager.StartDamagePhase(CardInPlace as MonsterCard);
//                 }
//             break;

//             default:
//             break;
//         }
//     }
// #endregion

// #region Custom Methods

//     public Board GetBoardController() { return _boardManager.BoardController; }
//     public void HighLight() { Visual.HighLight(); }
//     public void UnHighLight() { Visual.UnHighLight(); }

//     public void SetCardInPlace(Card card){
//         if(card is MonsterCard){
//             var monsterCard = card as MonsterCard;
//             if(monsterCard.IsInAttackMode){//In attacK

//                 if(monsterCard.IsFaceDown){// In Attack Face Down

//                     Quaternion rotation;

//                     if(monsterCard.IsPlayerCard){
//                         rotation = _boardManager.PlayerMonsterFaceDownAtkRotation;
//                     }else{
//                         rotation = _boardManager.EnemyMonsterFaceDownAtkRotation;
//                     }

//                     card.MoveCard(transform, rotation);

//                 }else{ // In Attack Face Up
//                     card.MoveCard(transform);
//                 }
                
//             }else{ // In Deffense
//                 if(monsterCard.IsFaceDown){// In Deffense Face Down

//                     Quaternion rotation;

//                     if(monsterCard.IsPlayerCard){
//                         rotation = _boardManager.PlayerMonsterFaceDownDefRotation;
//                     }else{
//                         rotation = _boardManager.EnemyMonsterFaceDownDefRotation;
//                     }

//                     card.MoveCard(transform, rotation);

//                 }else{ // In Deffense Face Up

//                     Quaternion rotation;

//                     if(monsterCard.IsPlayerCard){
//                         rotation = _boardManager.PlayerMonsterFaceUpDefRotation;
//                     }else{
//                         rotation = _boardManager.EnemyMonsterFaceUpDefRotation;
//                     }

//                     card.MoveCard(transform, rotation);
//                 }
//             }
//         }else{// Arcane Card

//         }

//         CardInPlace = card;
//         card.SetBoardPlace(this);
//         card.DeselectCard();
//         card.DisableCollider();
        
//         IsFree = false;
//         _canBeSelected = false;
//         _boardManager.BoardPlaceSelected();
//     }

//     public void SetPlaceFree(){
//         CardInPlace = null;
//         _canBeSelected = true;
//         IsFree = true;
//     }

//     public void StartBoardFusion(){
//         var newCardList = new List<Card>{CardInPlace, _resultCard};
//         _cardManager.Selector.SetCardsToBoardFusion(newCardList);
//         _battleManager.Battle.ChangeState(_battleManager.Battle.Fusion);//Change phase back to fusion
//         SetPlaceFree();
//     }

//     public void FlipCard(){
//         if(CardInPlace is MonsterCard){
//             var monsterCard = CardInPlace as MonsterCard;

//             if(!monsterCard.IsFaceDown) { return; } // is face Up

//             Quaternion rotation;

//             if(monsterCard.IsPlayerCard){
//                 //Monster in attack
//                 if(monsterCard.IsInAttackMode){
//                     monsterCard.MoveCard(transform);
//                 }else{
//                     //Monster in Defense
//                     rotation = _boardManager.PlayerMonsterFaceUpDefRotation;
//                     monsterCard.MoveCard(transform, rotation);
//                 }

//             }else{
//                 //Monster in attack
//                 if(monsterCard.IsInAttackMode){
//                     monsterCard.MoveCard(transform);
//                 }else{
//                     //Monster in Defense
//                     rotation = _boardManager.EnemyMonsterFaceUpDefRotation;
//                     monsterCard.MoveCard(transform, rotation);
//                 }

//             }

//             CardInPlace.SetWasFlipedThisTurn(true);
//             CardInPlace.SetCanFlip(false);
//             CardInPlace.SetFaceUp();
//             return;
//         }else{

//         }
//     }

//     public void ChangeMonsterToDef(){
//         var monsterCard = CardInPlace as MonsterCard;
//         Quaternion rotation;
//         if(monsterCard.IsPlayerCard){
//             if(monsterCard.IsFaceDown){
//                 rotation = _boardManager.PlayerMonsterFaceDownDefRotation;
//                 monsterCard.MoveCard(transform, rotation);
//                 return;
//             }
//             rotation = _boardManager.PlayerMonsterFaceUpDefRotation;
//             monsterCard.MoveCard(transform, rotation);
//             monsterCard.SetDeffenseMode();
//             monsterCard.SetCanChangeMode(false);
//             return;
//         }
//     }

//     public void ChangeMonsterToAtk(){
//         var monsterCard = CardInPlace as MonsterCard;
//         // Quaternion rotation;
//         if(monsterCard.IsPlayerCard){
//             // if(monsterCard.IsFaceDown){
//             //     rotation = PlayerMonsterFaceDownAtkRotation;
//             //     monsterCard.MoveCard(transform, rotation);
//             //     return;
//             // }
//             // rotation = PlayerMonsterFaceUpRotation;
//             monsterCard.MoveCard(transform);
//             monsterCard.SetAttackMode();
//             monsterCard.SetCanChangeMode(false);
//             return;
//         }
//     }

// #endregion

// #region Events

//     /// <summary>
//     /// Allow a card be selected
//     /// </summary>
//     private void BattleManager_OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn){
//         _resultCard = card;
//         if(IsMonsterPlace && _resultCard is MonsterCard){
//             _canBeSelected = true;
//         }else if(!IsMonsterPlace && _resultCard is ArcaneCard){
//             _canBeSelected = true;
//         }
//     }

//     private void BattleManager_OnAttackPlaceSelectionStart(){
//         if(_boardManager.BoardController.AICardsOnField.Count == 0){

//         }
//     }

//     private void BoardManager_OnBoardPlaceSelected(){
//         _canBeSelected = false;
//     }

//     public void LightUpPlace(){
//         Visual.LightUp();
//     }

//     #endregion

}