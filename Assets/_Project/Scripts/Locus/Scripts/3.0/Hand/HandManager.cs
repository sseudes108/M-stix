using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class HandManager : MonoBehaviour{
        [SerializeField] private Hand _playerHand;
        [SerializeField] private Hand _enemyHand;

        [SerializeField] private BattleManager _battleManager;
        private bool _selectionEnded = false;

        public void CheckPositionsInHand(int currentTurn, bool isPlayerTurn){
            if(currentTurn == 1){
                _playerHand.CheckPositionsInHand();
                _enemyHand.CheckPositionsInHand();
            }else{
                if(isPlayerTurn){
                    _playerHand.CheckPositionsInHand();
                }else{
                    _playerHand.CheckPositionsInHand();
                }
            }
        }

        public void DrawCards(int currentTurn, bool isPlayerTurn){
            if(currentTurn == 1){
                _playerHand.Draw();
                _enemyHand.Draw();
            }else{
                if(isPlayerTurn){
                    _playerHand.Draw();
                }else{
                    _enemyHand.Draw();
                }
            }
        }

        public Card DrawCard(ScriptableObject cardData){
            return _battleManager.DrawCard(cardData);
        }

        public void AllowCardSelection() { 
            _selectionEnded = false;
            _playerHand.AllowCardSelection(); 
        }

        // public void ResetHandFull(){
        //     if(_battleManager.IsPlayerTurn()){
        //         _playerHand.ResetHandFull();
        //     }else{
        //         _enemyHand.ResetHandFull();
        //     }
        // }

        public bool IsHandFull(){
            if(_battleManager.IsPlayerTurn()){
                return _playerHand.IsHandFull();
            }else{
                return _enemyHand.IsHandFull();
            }
        }

        public void BlockCardSelection() { _playerHand.BlockCardSelection(); }

        public bool IsCardSelectionEnded(){ return _selectionEnded; }

        public void EndCardSelection(){ 
            _selectionEnded = true; 
        }
        
        public void ResetCardSelection() { 
            _selectionEnded = false; 
        }

        public void MoveHandOffScreen(){ _playerHand.MoveHandOffScreen(); }
        public void MoveHandOnScreen(){ _playerHand.MoveHandOnScreen(); }

        public List<Card> GetCardsInAIHand(){
            return _enemyHand.GetCardsInHand();
        }

    }
}