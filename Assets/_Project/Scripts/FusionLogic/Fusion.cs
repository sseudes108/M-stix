using System;
using System.Collections;
using System.Collections.Generic;
using Mistix.Enums;
using UnityEngine;

namespace Mistix.FusionLogic{
    public class Fusion:MonoBehaviour{
        private FusionCardsPlacement _fusionPlacement;
        public Action OnFusionStarted, OnFusionEnded;

        private Coroutine _fusionRoutine;

        private void Awake() {
            _fusionPlacement = GetComponent<FusionCardsPlacement>();
        }

        public void StartFusionRoutine(List<Card> selectedCards){
            _fusionRoutine = StartCoroutine(FusionRoutine(selectedCards));
        }

        public IEnumerator FusionRoutine(List<Card> selectedCards){
            do{
                OnFusionStarted?.Invoke();
                _fusionPlacement.MoveSelectedCardsToPosition(selectedCards);

                var card1 = selectedCards[0];
                var card2 = selectedCards[1];
                var card1Type = card1.GetCardType();
                var card2Type = card2.GetCardType();

                //Arcane and Monster
                if (card1Type == ECardType.Arcane && card2Type == ECardType.Monster){
                    Debug.Log("Fusion Failed - Card 1 - Arcane Card 2 - Monster");
                    FusionFailed(card1, card2, selectedCards);
                }
                yield return new WaitForSeconds(0.5f);

                //Monster and Arcane
                if (card1Type == ECardType.Monster && card2Type == ECardType.Arcane){
                    Debug.Log("Fusion Failed - Card 1 - Monster Card 2 - Arcane");
                    FusionFailed(card1, card2, selectedCards);
                }
                yield return new WaitForSeconds(0.5f);

                //Arcane and Arcane
                if(card1Type == ECardType.Arcane && card2Type == ECardType.Arcane){
                    Debug.Log("Arcane and Arcane - Implement Logic!");
                    FusionFailed(card1, card2, selectedCards);
                }
                yield return new WaitForSeconds(0.5f);

                //Monster and Monster
                var monster1 = card1.GetComponent<MonsterCard>();
                var monster2 = card2.GetComponent<MonsterCard>();

                var monster1Level = monster1.GetMonsterLevel();
                var monster2Level = monster2.GetMonsterLevel();

                //Levels not equals
                if(monster1Level != monster2Level){
                    Debug.Log($"Fusion Failed Lvls not equal - Card 1 - Lvl {monster1Level} Card 2 - Lvl {monster2Level}");
                    FusionFailed(card1, card2, selectedCards);
                }
                yield return new WaitForSeconds(0.5f);


                //levels equals
                if(monster1Level == monster2Level){
                    var monster1Atk = monster1.GetMonsterAtk();
                    var monster2Atk = monster2.GetMonsterAtk();

                    EMonsterType strongestMonsterType;
                    if (monster2Atk < monster1Atk){
                        strongestMonsterType = monster1.GetMonsterType();
                    }else{
                        strongestMonsterType = monster2.GetMonsterType();
                    }

                    var strongestMonsterTypeList = GetStrongestTypeList(strongestMonsterType);

                    var possibleResultMonsters = GetPossibleResultMonsterList(strongestMonsterTypeList, monster1Level);

                    Card resultCard = FusionCards(possibleResultMonsters);

                    FusionSucess(resultCard, card1, card2, selectedCards);
                }
                
            } while(selectedCards.Count > 0);

            yield return new WaitForSeconds(1f);

            OnFusionEnded?.Invoke();
        }

        private List<MonsterCardSO> GetPossibleResultMonsterList(List<MonsterCardSO> strongestMonsterTypeList, int fusionLevel){
            var possibleMonsters = new List<MonsterCardSO>();
            foreach(var monster in strongestMonsterTypeList){
                if(monster.Level == fusionLevel + 1){
                    possibleMonsters.Add(monster);
                }
            }
            return possibleMonsters;
        }

        private static List<MonsterCardSO> GetStrongestTypeList(EMonsterType strongestMonsterType){
            switch (strongestMonsterType)
            {
                case EMonsterType.Angel:
                    return BattleManager.Instance.CardsDatabase.GetAngels();
                case EMonsterType.Dragon:
                    return BattleManager.Instance.CardsDatabase.GetDragons();
                case EMonsterType.Machine:
                    return BattleManager.Instance.CardsDatabase.GetMachines();
                default:
                    Debug.Log("Error Getting the stronger monster type list");
                    return null;
            }
        }

        private Card FusionCards<T>(List<T> possibleCards) where T : ScriptableObject{
            var randomIndex = UnityEngine.Random.Range(0, possibleCards.Count);
            var resultCard = BattleManager.Instance.CardCreator.CreateFusionedCard(possibleCards[randomIndex]);
            _fusionPlacement.MoveResultCardToPosition(resultCard);
            resultCard.SetUpCardOwner();
            return resultCard;
        }

        private void FusionFailed(Card card1, Card card2, List<Card> selectedCards){
            _fusionPlacement.MoveResultCardToPosition(card1);   
            _fusionPlacement.MoveResultCardToPosition(card2);

            BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(card1);
            BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(card2);

            card1.gameObject.SetActive(false);
            _fusionPlacement.MoveResultCardToPosition(card2);

            if(selectedCards.Count != 0){
                BattleManager.Instance.CardSelector.AddResultCardToSelectedList(card2);
            }else{
                StopCoroutine(_fusionRoutine);
                _fusionRoutine = null;
            }
        }

        private void FusionSucess(Card resultCard, Card card1, Card card2, List<Card> selectedCards){
            _fusionPlacement.MoveResultCardToPosition(card1);   
            _fusionPlacement.MoveResultCardToPosition(card2);

            BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(card1);
            BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(card2);

            card1.gameObject.SetActive(false);
            card2.gameObject.SetActive(false);
            _fusionPlacement.MoveResultCardToPosition(resultCard);

            if(selectedCards.Count != 0){
                BattleManager.Instance.CardSelector.AddResultCardToSelectedList(resultCard);
            }else{
                StopCoroutine(_fusionRoutine);
                _fusionRoutine = null;
            }
        }
    }
}