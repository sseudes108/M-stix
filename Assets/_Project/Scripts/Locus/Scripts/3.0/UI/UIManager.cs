using System;
using UnityEngine;

namespace Mistix{
    
    public class UIManager : MonoBehaviour {
        private UI_Deck _uiDeck;
        private UI_CardHolder _uiCardHolder;
        private UI_ButtonAction _uiButtonActions;
        private UI_ButtonCardStat _uiButtonCardStat;
        private UI_ActionPhaseButtons _uiActionPhaseButtons;
        private UI_Battle _uiBattle;
        [SerializeField] private BattleManager _battleManager;

        private Card _fusionResultCard;

        private bool _actionSelected = false;

        private void Awake() {
            _uiDeck = GetComponent<UI_Deck>();
            _uiBattle = GetComponent<UI_Battle>();
            _uiCardHolder = GetComponent<UI_CardHolder>();
            _uiButtonActions = GetComponent<UI_ButtonAction>();
            _uiButtonCardStat = GetComponent<UI_ButtonCardStat>();
            _uiActionPhaseButtons = GetComponent<UI_ActionPhaseButtons>();
        }

        public void ResetDeckCount() { _uiDeck.ResetDeckCount(); }
        public void UpdateIllustration(Texture2D illustration) { _uiCardHolder.UpdateIllustration(illustration); }
        public void ShowEndSelectionButton() { _uiButtonActions.ShowEndCardSelectionButton(); }
        public void HideEndSelectionButton() { _uiButtonActions.HideActionButton(); }
        
        public void UpdateTurn(int turn, bool IsPlayerTurn){ _uiBattle.UpdateTurn(turn, IsPlayerTurn); }
        public void UpdateLifePoints(bool isPlayer, int lifePoints){ _uiBattle.UpdateLifePoints(isPlayer, lifePoints); }
        public void UpdateDeckCount(bool isPlayer, int deckCount){ _uiBattle.UpdateDeckCount(isPlayer, deckCount); }
        public void UpdateDebugBattleState(string state){ _uiBattle.UpdateDebugBattleState(state); }

        public void MoveUICardOffScreen(){ _uiCardHolder.MoveOffScren(); }

        public void EndCardSelection(){ _battleManager.EndCardSelection(); }
        public bool IsCardSelectionPhase(){ return _battleManager.IsCardSelectionPhase(); }

        public void ShowCardStatOptions(Card card){
            _fusionResultCard = card;
            _uiButtonCardStat.ShowOptions(_fusionResultCard);
        }

        public void SelectAnother(MonsterCard monster){ _uiButtonCardStat.SetButtonText(monster); }

        public void StatSelectionEnd(){ _uiButtonCardStat.HideOptions(); }

        public void Option1_Clicked(){ _battleManager.Option1_Clicked(_fusionResultCard); }
        public void Option2_Clicked(){ _battleManager.Option2_Clicked(_fusionResultCard); }

        public void ShowOptions(Card cardInPlace, BoardPlace place){
            _actionSelected = false;
            _uiActionPhaseButtons.SetCardOptions(cardInPlace, place);
        }

        public void HideOptions(){
            _uiActionPhaseButtons.HideOptions();
        }

        public void ActionSelected(){
            _actionSelected = true;
        }

        public bool IsCardSelected(){ return _actionSelected; }

        public void ShowEndActionButton(){ _uiButtonActions.ShowEndActionPhaseButton(); }

        public bool IsActionPhase(){ return _battleManager.IsActionPhase(); }
    }
}