using System;
using UnityEngine;

namespace Mistix{
    public class HandManager : MonoBehaviour{
        [SerializeField] private Hand _playerHand;
        [SerializeField] private Hand _enemyHand;

        [SerializeField] private BattleManager _battleManager;

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

        public Card InstantiateCard(ScriptableObject cardData){
            return _battleManager.InstantiateCard(cardData);
        }
    }
}