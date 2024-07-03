// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public abstract class BoardCardPlace : MonoBehaviour {
//     public static Action<BoardCardPlace, Card> OnFlipCard;
//     public static Action<BoardCardPlace, CardMonster> OnMonsterSetOnBoard;
    
//     [SerializeField] protected bool _isFree;
//     [SerializeField] protected Card _cardInThisPlace;
//     [SerializeField] protected GameObject _canvas;
//     [SerializeField] private Button _flipCard;
//     protected Collider _collider;
//     protected Renderer _renderer;

//     protected void Awake() {
//         _collider = GetComponent<Collider>();
//         _renderer = GetComponentInChildren<Renderer>();
//     }

//     protected virtual void Start() {
//         SetPlaceFree();
//     }
    
//     protected virtual void OnMouseOver() {
//         var currentPhase = BattleManager.Instance.BattleStateManager.CurrentState;

//         //Change ilustration in the ui hold card when the card itself has the collider off;
//         if(!_isFree){
//             if(!_cardInThisPlace.IsFaceDown() || _cardInThisPlace.IsPlayerCard()){
//                 if(_cardInThisPlace is CardMonster){
//                     var card = _cardInThisPlace as CardMonster;
//                     BattleManager.Instance.UIBattleManager.UICardPlaceHolder.ChangeIllustration(
//                         _cardInThisPlace.Ilustration, card.GetAnimas(), card.GetLevel(), card.GetAttack(), card.GetDefense()
//                     );
//                 }else{
//                     BattleManager.Instance.UIBattleManager.UICardPlaceHolder.ChangeIllustration(_cardInThisPlace.Ilustration);
//                 }
//             }


//             //Change Mode Options
//             if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
//                 if(currentPhase == BattleManager.Instance.ActionPhase || currentPhase == BattleManager.Instance.AttackPhase && _canvas != null){
//                     //Show flip button in the face down cards
//                     if(_canvas != null){
//                         _canvas.SetActive(true);
//                         if(_cardInThisPlace.IsFaceDown()){
//                             _flipCard.gameObject.SetActive(true);
//                             _flipCard.onClick.AddListener(TriggerFlipCardEvent);
//                         }else{
//                             //Does not show on face up
//                             _flipCard.gameObject.SetActive(false);
//                         }
//                     }
//                 }
//             }
//         }
//     }

//     protected virtual void OnMouseExit() {
//         if(_canvas != null){
//             _flipCard.onClick.RemoveAllListeners();
//             if(_canvas.activeSelf){
//                 _canvas.SetActive(false);
//             }
//         }
//     }
    
//     protected virtual void OnMouseDown() {
//         var currentPhase = BattleManager.Instance.BattleStateManager.CurrentState;

//         if(currentPhase != BattleManager.Instance.AttackPhase){
//             var resultCard = BattleManager.Instance.FusionManager.GetResultCard();

//             if(currentPhase == BattleManager.Instance.BoardPlaceSelectionPhase){
//                 if(_isFree){
//                     //is monster place and monster card, or is arcane place and arcane card
//                     if(this is BoardCardMonsterPlace && resultCard is CardMonster /* OR */
//                         || this is BoardCardArcanePlace && resultCard is CardArcane){
//                             SetCardInPlace(resultCard);
//                     }
//                 }else{
//                     if(BattleManager.Instance.InputManager.CanClick){
//                         BoardFusion(resultCard);
//                     }
//                 }
//             }
//         }
//     }

//     public void BoardFusion(Card resultCard){
//         List<Card> fusionList = new(){_cardInThisPlace, resultCard};
//         SetPlaceFree();
//         BattleManager.Instance.FusionManager.SetFusionList(fusionList);
//         BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
//     }

//     public void SetCardInPlace(Card resultCard){
//         StartCoroutine(MoveCardRoutine(resultCard));
//     }

//     private IEnumerator MoveCardRoutine(Card resultCard){
//         BattleManager.Instance.InputManager.BlockClickInput();
        
//         if(resultCard.IsOnField() == false){;
//             resultCard.MoveCard(transform);

//             if(resultCard is CardMonster){
//                 SetMonsterCardRotation(resultCard as CardMonster);
//             }else{
//                 SetArcaneCardRotation(resultCard as CardArcane);
//             }

//             resultCard.SetCardOnField();
//             SetPlaceOcuppied(resultCard);
//             BattleManager.Instance.BoardPlaceManager.SetLastCardPlaced(resultCard);

//             yield return new WaitForSeconds(1f);
//             BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.ActionPhase);
//             BattleManager.Instance.InputManager.AllowClickInput();
//         }
//     }

//     public void SetPlaceFree(){
//         _cardInThisPlace = null; 
//         _isFree = true;
//     }

//     public void SetPlaceOcuppied(Card card){
//         _cardInThisPlace = card;
//         _isFree = false;
//         card.DisableCollider();

//         if(card is CardMonster){
//             var place = this as BoardCardMonsterPlace;
//             place.BlockChangeModeAndAttack();
//             TriggerMonsterSetOnBoardEvent();
//         }
//     }

//     public Card GetCardInThisPlace(){return _cardInThisPlace;}

//     public void EnableCardColliderInBoardPhaseSelection(){
//         if(_cardInThisPlace != null){
//             _cardInThisPlace.EnableCollider();
//         }
//     }
    
//     protected virtual void SetMonsterCardRotation(CardMonster resultCard){}
//     protected virtual void SetArcaneCardRotation(CardArcane resultCard){}
//     public bool IsFree() => _isFree;
//     public virtual Renderer Renderer => _renderer;

//     //Events
//     private void TriggerFlipCardEvent(){
//         if(_cardInThisPlace != null){
//             OnFlipCard?.Invoke(this, _cardInThisPlace);
//         }
//     }

//     private void TriggerMonsterSetOnBoardEvent(){
//         if(_cardInThisPlace != null){
//             OnMonsterSetOnBoard?.Invoke(this, _cardInThisPlace as CardMonster);
//         }
//     }
// }