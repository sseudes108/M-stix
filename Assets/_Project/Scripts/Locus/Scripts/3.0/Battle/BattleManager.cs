using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class BattleManager : MonoBehaviour {
        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private AIManager _aiManager;
        [SerializeField] private LPManager _lpManager;
        [SerializeField] private HandManager _handManager;
        [SerializeField] private CardManager _cardManager;
        [SerializeField] private FusionManager _fusionManager;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private BattleSM _battleSM;

        private TurnManager _turnManager;

        private void Awake() { _turnManager = new TurnManager(); }

    #region Board Manager
        public void LighOffAllPlaces() { _boardManager.LightOffAllPlaces(); }
        public void LightUpAllPlaces() { _boardManager.LightUpAllPlaces(); }
        public void SetPlaceColors(Vector3 blueBoardColor, Vector3 redBoardColor){
            _boardManager.SetPlaceColors(blueBoardColor, redBoardColor);
        }
        public void HighLightPossiblePlaces(){ _boardManager.HighLightFreePlaces(); }
        public bool IsBoardPlaceSelected(){
            return _boardManager.IsBoardPlaceSelected();
        }
        public bool PlayerHasArcaneOnField(){
            return _boardManager.PlayerHasArcaneOnField();
        }

        public bool EnemyHasArcaneOnField(){
            return _boardManager.EnemyHasArcaneOnField();
        }

    #endregion

    #region Life Points Manager
        public void ResetLifePoints() { _lpManager.ResetLifePoints(); }

    #endregion

    #region Card Manager
        public Card DrawCard(ScriptableObject cardData) { return _cardManager.DrawCard(cardData); }
        public Card CreateMonsterCard(MonsterCardSO monsterCardSO){ return _cardManager.DrawCard(monsterCardSO); }

        public void Option1_Clicked(Card card) { _cardManager.Option1_Clicked(card); }
        public void Option2_Clicked(Card card){ _cardManager.Option2_Clicked(card); }

        public bool IsAllStatsSelected(){ return  _cardManager.IsAllStatsSelected(); }
        public void ResetCardStatSelectionEnded(){ _cardManager.ResetCardStatSelectionEnded(); }

    #endregion

    #region Turn Manager
        public bool IsFirstTurn() { return _turnManager.IsFirstTurn(); }
        public bool IsPlayerTurn() { return _turnManager.GetTurnInfo().Item2; }
        public void EndTurn(){  _turnManager.EndTurn(); }

    #endregion

    #region Hand Manager
        public void AllowCardSelection() { _handManager.AllowCardSelection(); }
        public void BlockCardSelection() { _handManager.BlockCardSelection(); }
        public bool IsHandFull() { return _handManager.IsHandFull(); }
        public void CheckPositionsInHand(){
            var turn = _turnManager.GetTurnInfo();
            _handManager.CheckPositionsInHand(turn.Item1, turn.Item2);
        }

        public void DrawCards(){
            var turn = _turnManager.GetTurnInfo();
            _handManager.DrawCards(turn.Item1, turn.Item2);            
        }

        public bool IsCardSelectionEnded() {
            return _handManager.IsCardSelectionEnded();
        }

        //Called by AImanager e UIManager (Player e AI)
        public void EndCardSelection(){
            _handManager.EndCardSelection();
        }
        
        public void ResetCardSelectionEnded(){
            _handManager.ResetCardSelection();
        }

        
    #endregion

    #region UI Manager
        public void UpdateTurn(){
            var turn = _turnManager.GetTurnInfo();
            _uiManager.UpdateTurn(turn.Item1, turn.Item2);
        }

        public void UpdateLifePoints(bool isPlayer, int lifePoints) { _uiManager.UpdateLifePoints(isPlayer, lifePoints); }
        public void UpdateDeckCount(bool isPlayer, int deckCount) { _uiManager.UpdateDeckCount(isPlayer, deckCount); }
        public void ResetDeckCount() { _uiManager.ResetDeckCount(); }
        public void UpdateDebugBattleState(string state) { _uiManager.UpdateDebugBattleState(state); }
        public void UpdateDebugAIState(string aiPhase) { _uiManager.UpdateDebugAIState(aiPhase); }
        public void UpdateCardUilustration(Texture2D illustration) { _uiManager.UpdateIllustration(illustration); }

        public void ShowEndSelectionButton() { _uiManager.ShowEndSelectionButton(); }
        public void HideEndSelectionButton() { _uiManager.HideEndSelectionButton(); }

        public void MoveUICardOffScreen() { _uiManager.MoveUICardOffScreen(); }
        public void MoveUICardOnScreen(){ _uiManager.MoveUICardOnScreen(); }
        
        public void MoveHandOffScreen() { _handManager.MoveHandOffScreen(); }

        public void ShowCardStatOptions(Card card) { 
            _uiManager.ShowCardStatOptions(card);
            _cardManager.ResetCardStatSelectionEnded();
        }

        public void SelectAnother(MonsterCard monster) { _uiManager.SelectAnother(monster); }

        public void StatSelectionEnd() { _uiManager.StatSelectionEnd(); }

        public void ShowOptions(Card cardInPlace, BoardPlace place){ _uiManager.ShowOptions(cardInPlace, place); }

        public void HideOptions(){ _uiManager.HideOptions(); }
        public bool IsActionSelected(){ return _uiManager.IsActionSelected(); }
        public void ResetActionSelected(){ _uiManager.ResetActionSelected(); }
        public void ShowEndActionButton(){ _uiManager.ShowEndActionButton(); }
    #endregion

    #region Fusion Manager
        public void StartFusionRoutine(){ 
            _fusionManager.StartFusionRoutine(
                _cardManager.GetSelectedCards(),
                _turnManager.IsPlayerTurn()
            ); 
        }
        public bool IsFusionEnded(){ return _fusionManager.IsFusionEnded(); }
        public Card GetFusionResultCard(){ return _fusionManager.GetResultCard(); }
        public void MoveToBoardPlaceSelection(){ _fusionManager.MoveToBoardPlaceSelection(); }
    #endregion

    #region Camera Manager
        public void ShakeCamera(){
            _cameraManager.ShakeCamera();
        }
    #endregion

    #region State Machine
        public bool IsCardSelectionPhase(){ return _battleSM.CurrentState is BS_03_CardSelection; }

        public bool IsBoardPlaceSelectionPhase(){ return _battleSM.CurrentState is BS_06_BoardPlaceSel; }

        public bool IsActionPhase(){ return _battleSM.CurrentState is BS_07_Action; }

    #endregion

    #region  AI
        //Card Selection
        public void ChangeAISMToCardSelectionPhase(){ _aiManager.ChangeAISMToCardSelectionPhase(); }
        public void StartCardSelection(){ _aiManager.StartCardSelection(); }
        public void SetSelectedAICards(List<Card> selectedList){ _cardManager.SetSelectedAICards(selectedList); }

        //Card Stat Selection
        public void ChangeAISMToCardStatSelPhase(){ _aiManager.ChangeAISMToCardStatSelPhase(); }
        public void StartCardStatsSelection(){ _aiManager.StartCardStatsSelection(); }

        //Cards
        public List<Card> GetCardsInAIHand(){ return _handManager.GetCardsInAIHand(); }
        public void SetFusionedCard(Card resultCard){ _aiManager.SetFusionedCard(resultCard); }

    #endregion

    }
}