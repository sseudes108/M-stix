using System;
using System.Collections;
using System.Collections.Generic;
using Mistix.Enums;
using UnityEngine;

namespace Mistix.FusionLogic{
    public class Fusion:MonoBehaviour{

        private FusionCardsPlacement _fusionPlacement;
        public Action OnFusionStarted, OnFusionEnded;

        private void Awake() {
            _fusionPlacement = GetComponent<FusionCardsPlacement>();
        }
        public void StartFusionRoutine(List<Card> selectedCards){
            StartCoroutine(FusionRoutine(selectedCards));
        }

        private IEnumerator FusionRoutine(List<Card> selectedCards){
            OnFusionStarted?.Invoke();
            _fusionPlacement.MoveSelectedCardsToPosition(selectedCards);

            yield return new WaitForSeconds(0.5f);
            var card1 = selectedCards[0];
            var card2 = selectedCards[1];

            var card1Type = card1.GetCardType();
            var card2Type = card2.GetCardType();

            //Arcane and Monster
            if(card1Type == ECardType.Arcane){
                Debug.Log("Fusion Failed - Card 1 Arcane");
                FusionFailed(card1, card2, selectedCards);
            }

            //Monster and Arcane
            if(card1Type == ECardType.Monster && card2Type == ECardType.Arcane){
                Debug.Log("Fusion Failed - Card 2 Arcane");
                FusionFailed(card1, card2, selectedCards);
            }
            
            //Monster and Monster
            if(card1Type == ECardType.Monster && card2Type == ECardType.Monster){
                var monster1 = card1.GetComponent<MonsterCard>();
                var monster2 = card2.GetComponent<MonsterCard>();

                var monster1Level = monster1.GetMonsterLevel();
                var monster2Level = monster2.GetMonsterLevel();

                //Differente levels
                if(monster1Level != monster2Level){
                    Debug.Log("Fusion Failed - Levels are not equal");
                    FusionFailed(card1, card2, selectedCards);
                }

                //Levels equals
                if(monster1Level == monster2Level){

                    var possibleMonsterList = GetPossibleMonsterList(monster1, monster2, monster1Level);

                    //Fusion Success
                    _fusionPlacement.MoveResultCardToPosition(card1);
                    _fusionPlacement.MoveResultCardToPosition(card2);

                    yield return new WaitForSeconds(0.3f);
                    
                    BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(card1);
                    BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(card2);

                    var randomIndex = UnityEngine.Random.Range(0, possibleMonsterList.Count);
                    var resultCard = BattleManager.Instance.CardCreator.CreateFusionedCard(possibleMonsterList[randomIndex]);
                    _fusionPlacement.MoveResultCardToPosition(resultCard);

                    resultCard.SetIsPlayerCard();

                    card1.gameObject.SetActive(false);
                    card2.gameObject.SetActive(false);

                    if(selectedCards.Count > 0){
                        ReestartFusionRoutine(resultCard, selectedCards);
                    }

                    OnFusionEnded?.Invoke();
                }
            }
        }

        private void ReestartFusionRoutine(Card resultCard, List<Card> selectedCards){
            BattleManager.Instance.CardSelector.AddResultCardToSelectedList(resultCard);
            StartFusionRoutine(selectedCards);
        }

        private  List<MonsterCardSO> GetPossibleMonsterList(MonsterCard monster1, MonsterCard monster2, int monster1Level){
            var monster1Atk = monster1.GetMonsterAtk();
            var monster2Atk = monster2.GetMonsterAtk();

            EMonsterType strongestMonsterType;
            if (monster2Atk < monster1Atk){
                strongestMonsterType = monster1.GetMonsterType();
            }
            else{
                strongestMonsterType = monster2.GetMonsterType();
            }

        //All cards from the designaded type
            var strongestMonsterTypeList = GetStrongestMonsterTypeList(strongestMonsterType);

        //All cards from the last list that has the right lvl (monster + 1)
            List<MonsterCardSO> possibleMonsterList = new();
            foreach (var monster in strongestMonsterTypeList){
                if (monster.Level == monster1Level + 1){
                    possibleMonsterList.Add(monster);
                }
            }

            return possibleMonsterList;
        }

        private List<MonsterCardSO> GetStrongestMonsterTypeList(EMonsterType strongestMonsterType){
            List<MonsterCardSO> strongestMonsterList = new();

            switch (strongestMonsterType){
                case EMonsterType.Angel:
                    strongestMonsterList = BattleManager.Instance.CardsDatabase.GetAngels();
                    break;
                case EMonsterType.Machine:
                    strongestMonsterList = BattleManager.Instance.CardsDatabase.GetMachines();
                    break;
                case EMonsterType.Dragon:
                    strongestMonsterList = BattleManager.Instance.CardsDatabase.GetDragons();
                    break;
                default:
                    Debug.Log("Error getting the strongest monster list");
                    break;
            }

            return strongestMonsterList;
        }

        private void FusionFailed(Card card1, Card card2, List<Card> selectedCards){
            _fusionPlacement.MoveResultCardToPosition(card1);
            _fusionPlacement.MoveResultCardToPosition(card2);

            BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(card1);
            BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(card2);

            card1.gameObject.SetActive(false);
            _fusionPlacement.MoveResultCardToPosition(card2);

            if(selectedCards.Count > 0){
                ReestartFusionRoutine(card2, selectedCards);
            }
        }
    }
}