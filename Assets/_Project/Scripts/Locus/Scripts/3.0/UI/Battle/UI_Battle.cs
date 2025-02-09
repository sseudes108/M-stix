using System;
using TMPro;
using UnityEngine;

namespace Mistix{   
    public class UI_Battle : MonoBehaviour {
        private UI_Deck _uiDeck;
        private UI_CardHolder _uiCardHolder;
        private UI_Buttons _uiButtons;

        [Header("Battle")]
        [SerializeField] private TextMeshProUGUI _turn;

        [Header("Player")]
        [SerializeField] private TextMeshProUGUI _playerLP;
        [SerializeField] private TextMeshProUGUI _playerDeck;

        [Header("Enemy")]
        [SerializeField] private TextMeshProUGUI _enemyLP;
        [SerializeField] private TextMeshProUGUI _enemyDeck;

        [Header("DEBUG")]
        [SerializeField] private TextMeshProUGUI _battlePhase;
        [SerializeField] private TextMeshProUGUI _aiState;

        private void Awake() {
            _uiDeck = GetComponent<UI_Deck>();
            _uiButtons = GetComponent<UI_Buttons>();
            _uiCardHolder = GetComponent<UI_CardHolder>();
            
        }

        public void UpdateTurn(int turn, bool playerTurn){
            var turnOwner = playerTurn ? "Player" : "AI";
            _turn.text = $"Turn: {turn} - {turnOwner}";
        }

        public void UpdateLifePoints(bool isPlayer, int lifePoints){
            if(isPlayer){
                _playerLP.text = $"LP: {lifePoints}";
            }else{
                _enemyLP.text = $"LP: {lifePoints}";
            }
        }

        public void UpdateDeckCount(bool isPlayer, int deckCount){
            if(isPlayer){
                _playerDeck.text = $"Deck: {deckCount}";
            }else{
                _enemyDeck.text = $"Deck: {deckCount}";
            }
        }

        public void UpdateDebugBattleState(string battlePhase){
            _battlePhase.text = $"Battle: {battlePhase}";
        }

        public void ResetDeckCount() { _uiDeck.ResetDeckCount(); }
        public void UpdateIllustration(Texture2D illustration) { _uiCardHolder.UpdateIllustration(illustration); }

        public void ShowEndSelectionButton() { _uiButtons.ShowEndSelectionButton(); }

        public void HideEndSelectionButton() { _uiButtons.HideEndSelectionButton(); }
    }
}