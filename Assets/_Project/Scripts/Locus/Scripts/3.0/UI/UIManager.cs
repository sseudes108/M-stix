using System;
using UnityEngine;

namespace Mistix{
    
    public class UIManager : MonoBehaviour {
        private UI_Deck _uiDeck;
        private UI_CardHolder _uiCardHolder;
        private UI_BattleButtons _uiButtons;
        private UI_Battle _uiBattle;
        [SerializeField] private BattleManager _battleManager;

        private void Awake() {
            _uiDeck = GetComponent<UI_Deck>();
            _uiBattle = GetComponent<UI_Battle>();
            _uiButtons = GetComponent<UI_BattleButtons>();
            _uiCardHolder = GetComponent<UI_CardHolder>();
        }

        public void ResetDeckCount() { _uiDeck.ResetDeckCount(); }
        public void UpdateIllustration(Texture2D illustration) { _uiCardHolder.UpdateIllustration(illustration); }
        public void ShowEndSelectionButton() { _uiButtons.ShowEndSelectionButton(); }
        public void HideEndSelectionButton() { _uiButtons.HideEndSelectionButton(); }
        public void UpdateTurn(int turn, bool IsPlayerTurn){
            _uiBattle.UpdateTurn(turn, IsPlayerTurn);
        }
        public void UpdateLifePoints(bool isPlayer, int lifePoints){
            _uiBattle.UpdateLifePoints(isPlayer, lifePoints);
        }

        public void UpdateDeckCount(bool isPlayer, int deckCount){
            _uiBattle.UpdateDeckCount(isPlayer, deckCount);
        }

        public void UpdateDebugBattleState(string state){
            _uiBattle.UpdateDebugBattleState(state);
        }

        public void EndCardSelection(){
            _battleManager.EndCardSelection();
        }
    }
}