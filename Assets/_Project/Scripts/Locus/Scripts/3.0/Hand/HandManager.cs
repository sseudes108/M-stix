using System;
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

        public bool IsHandFull(){
            if(_battleManager.IsPlayerTurn()){
                Debug.Log("Player Hand Full");
                return _playerHand.IsHandFull();
            }else{
                Debug.Log("Enemy Hand Full");
                return _enemyHand.IsHandFull();
            }
        }

        public void BlockCardSelection() { _playerHand.BlockCardSelection(); }

        public bool IsCardSelectionEnded(){ return _selectionEnded; }

        public void EndCardSelection(){ _selectionEnded = true; }

        public void MoveHandOffScreen(){ _playerHand.MoveHandOffScreen(); }
        public void MoveHandOnScreen(){ _playerHand.MoveHandOnScreen(); }
    }
}