using UnityEngine;

namespace Mistix{
    
    public class BattleManager : MonoBehaviour {
        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private UI_Battle _uiBattle;
        [SerializeField] private LPManager _lpManager;
        [SerializeField] private HandManager _handManager;
        [SerializeField] private CardManager _cardManager;

        private TurnManager _turnManager;

        private void Awake() { _turnManager = new TurnManager(); }

    #region Board Manager
        public void LighOffAllPlaces() { _boardManager.LightOffAllPlaces(); }
        public void LightUpAllPlaces() { _boardManager.LightUpAllPlaces(); }
        public void SetPlaceColors(Vector3 blueBoardColor, Vector3 redBoardColor){
            _boardManager.SetPlaceColors(blueBoardColor, redBoardColor);
        }

    #endregion

    #region Life Points Manager
        public void ResetLifePoints() { _lpManager.ResetLifePoints(); }

    #endregion

    #region Card Manager
        public Card DrawCard(ScriptableObject cardData) { return _cardManager.DrawCard(cardData); }
        public void SelectCard(Card card) { _cardManager.SelectCard(card); }
        public void DeselectCard(Card card) { _cardManager.DeselectCard(card); }

    #endregion

    #region Turn Manager
        public bool IsFirstTurn() { return _turnManager.IsFirstTurn(); }
        public bool IsPlayerTurn() { return _turnManager.GetTurnInfo().Item2; }

    #endregion

    #region Hand Manager
        public void AllowCardSelection(){ _handManager.AllowCardSelection(); }
        public bool IsHandFull() { return _handManager.IsHandFull(); }
        public void CheckPositionsInHand(){
            var turn = _turnManager.GetTurnInfo();
            _handManager.CheckPositionsInHand(turn.Item1, turn.Item2);
        }
        public void DrawCards(){
            var turn = _turnManager.GetTurnInfo();
            _handManager.DrawCards(turn.Item1, turn.Item2);            
        }
        
    #endregion

    #region UI Manager
        public void UpdateTurn(){
            var turn = _turnManager.GetTurnInfo();
            _uiBattle.UpdateTurn(turn.Item1, turn.Item2);
        }
        public void UpdateLifePoints(bool isPlayer, int lifePoints) { _uiBattle.UpdateLifePoints(isPlayer, lifePoints); }
        public void UpdateDeckCount(bool isPlayer, int deckCount) { _uiBattle.UpdateDeckCount(isPlayer, deckCount); }
        public void ResetDeckCount() { _uiBattle.ResetDeckCount(); }
        public void UpdateDebugBattleState(string state) { _uiBattle.UpdateDebugBattleState(state); }
        public void UpdateCardUilustration(Texture2D illustration) {
            _uiBattle.UpdateIllustration(illustration);
        }
        public void ShowEndSelectionButton() {
            _uiBattle.ShowEndSelectionButton();
        }
        public void HideEndSelectionButton() {
            _uiBattle.HideEndSelectionButton();
        }
    
    #endregion
    
    }
}