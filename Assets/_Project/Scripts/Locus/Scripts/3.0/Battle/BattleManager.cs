using System;
using UnityEngine;

namespace Mistix{
    
    public class BattleManager : MonoBehaviour {
        
        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private UI_Battle _uiBattle;
        [SerializeField] private UI_Deck _uiDeck;
        [SerializeField] private LPManager _lpManager;
        [SerializeField] private HandManager _handManager;
        [SerializeField] private CardManager _cardManager;
        private TurnManager _turnManager;

        private void Awake() {
            _turnManager = new TurnManager();
        }

        public void LighOffAllPlaces() { _boardManager.LightOffAllPlaces(); }

        public void LightUpAllPlaces() { _boardManager.LightUpAllPlaces(); }

        public void SetPlaceColors(Vector3 blueBoardColor, Vector3 redBoardColor){
            _boardManager.SetPlaceColors(blueBoardColor, redBoardColor);
        }

        public void UpdateTurn(){
            var turn = _turnManager.GetTurnInfo();
            _uiBattle.UpdateTurn(turn.Item1, turn.Item2);
        }

        public void UpdateLifePoints(bool isPlayer, int lifePoints) { _uiBattle.UpdateLifePoints(isPlayer, lifePoints); }
        public void UpdateDeckCount(bool isPlayer, int deckCount) { _uiBattle.UpdateDeckCount(isPlayer, deckCount); }
        public void ResetLifePoints() { _lpManager.ResetLifePoints(); }
        public void ResetDeckCount() { _uiDeck.ResetDeckCount(); }
        
        public void UpdateDebugBattleState(string state) { _uiBattle.UpdateDebugBattleState(state); }

        public void CheckPositionsInHand(){
            var turn = _turnManager.GetTurnInfo();
            _handManager.CheckPositionsInHand(turn.Item1, turn.Item2);
        }

        public void DrawCards(){
            var turn = _turnManager.GetTurnInfo();
            _handManager.DrawCards(turn.Item1, turn.Item2);            
        }

        public Card InstantiateCard(ScriptableObject cardData){
            return _cardManager.InstantiateCard(cardData);
        }

        public void AllowCardSelection(){
            _handManager.AllowCardSelection();
        }
    }
}