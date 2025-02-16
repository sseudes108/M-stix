using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class FusionManager : MonoBehaviour {
        [SerializeField] private BattleManager _battleManager;
        private Fusion _fusion;
        private MonsterFusion _monsterFusion;
        private ArcaneFusion _arcaneFusion;
        private FusionPositions _fusionPositions;

        private Card _resultCard;
        private bool _isFusionEnded;
        
        private void Awake() {
            _fusion = GetComponent<Fusion>();
            _monsterFusion = GetComponent<MonsterFusion>();
            _arcaneFusion = GetComponent<ArcaneFusion>();
            _fusionPositions = GetComponent<FusionPositions>();
        }

    #region Card
        public Card CreateCard(MonsterCardSO monsterCardSO){
            return _battleManager.CreateMonsterCard(monsterCardSO);
        }
        public void SetResultCard(Card card) { _resultCard = card; }
        public Card GetResultCard() { return _resultCard;}
    #endregion

    #region Positions
        public void MoveCardsToFusionPosition(List<Card> cards, bool isPlayerTurn){
            _fusionPositions.MoveCardsToFusionPosition(cards, isPlayerTurn);
        }

        public void StartMonsterFusionRoutine(MonsterCard monster1, MonsterCard monster2){
            _monsterFusion.StartMonsterFusionRoutine(monster1, monster2);
        }

        public void MoveCardToResultPosition(Card resultCard, bool isPlayerTurn){
            _fusionPositions.MoveCardToResultPosition(resultCard, isPlayerTurn);
        }

        public void MoveCardsToMergePosition(List<Card> materials, bool isPlayerTurn){
            _fusionPositions.MoveCardsToMergePosition(materials, isPlayerTurn);
        }

        public void MoveToBoardPlaceSelection(){ 
            _fusionPositions.MoveToBoardPlaceSelection(_resultCard, _battleManager.IsPlayerTurn()); 
        }
    #endregion

    #region Fusion
        public void StartFusionRoutine(List<Card> selectedCards, bool isPlayerTurn){
            _isFusionEnded = false;
            _fusion.StartFusionRoutine(selectedCards, isPlayerTurn);
        }

        public void FusionFailed(MonsterCard monster1, MonsterCard monster2){
            _fusion.FusionFailed(monster1, monster2);
        }

        public void FusionSucess(MonsterCard monster1, MonsterCard monster2, Card fusionedCard){
            _fusion.FusionSucess(monster1, monster2, fusionedCard);
        }

        public bool IsFusionEnded(){
            return _isFusionEnded;
        }

        //Chamado em fusion.cs ao finalizar a rotina
        public void FusionEnded(){
            _isFusionEnded = true;
        }

        //Chamado em fusion.cs em fusion failed
        public void ShakeCamera(){
            _battleManager.ShakeCamera();
        }

    #endregion
    }
}