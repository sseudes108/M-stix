using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class FusionManager : MonoBehaviour {
        private Fusion _fusion;
        private MonsterFusion _monsterFusion;
        private ArcaneFusion _arcaneFusion;
        private FusionPositions _fusionPositions;

        private Card _resultCard;
        
        private void Awake() {
            _fusion = GetComponent<Fusion>();
            _monsterFusion = GetComponent<MonsterFusion>();
            _arcaneFusion = GetComponent<ArcaneFusion>();
            _fusionPositions = GetComponent<FusionPositions>();
        }

        public void SetResultCard(Card card) { _resultCard = card; }
        public Card GetResultCard() { return _resultCard;}

        public void StartFusionRoutine(List<Card> selectedCards, bool isPlayerTurn){
            _fusion.StartFusionRoutine(selectedCards, isPlayerTurn);
        }

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
    }
}