using System;
using System.Collections;
using System.Collections.Generic;
using Mistix.Enums;
using UnityEngine;

namespace Mistix.FusionLogic{
    public class Fusion:MonoBehaviour{
        public static Fusion Instance;
        private Coroutine _fusionRoutine;

        public Action OnFusionStarted, OnFusionEnded;

        [SerializeField] private Transform _card1InLinePosition, _card2InLinePosition;

        private void Awake() {
            if(Instance != null){
                Errors.InstanceError(this);
            }
            Instance = this;
        }

        public void StartFusionRoutine(List<Card> selectedCards){
            if(_fusionRoutine == null){
                StartCoroutine(FusionRoutine(selectedCards));
            }
        }

        private IEnumerator FusionRoutine(List<Card> selectedCards){
            OnFusionStarted?.Invoke();

            MoveSelectedCardsToPosition(selectedCards);

            yield return new WaitForSeconds(0.5f);
            var card1 = selectedCards[0];
            var card2 = selectedCards[1];

            //Verificar se ambos são mostros//

            var card1Type = card1.GetCardType();
            var card2Type = card2.GetCardType();

            if(card1Type == Enums.ECardType.Arcane){
                Debug.Log("Fusion Failed - Card 1 Arcane");
                StopCoroutine(_fusionRoutine);
            }

            if(card1Type == Enums.ECardType.Monster && card2Type == Enums.ECardType.Arcane){
                Debug.Log("Fusion Failed - Card 2 Arcane");
                StopCoroutine(_fusionRoutine);
            }

            if(card1Type == Enums.ECardType.Monster && card2Type == Enums.ECardType.Monster){
                var monster1 = card1.GetComponent<MonsterCard>();
                var monster2 = card2.GetComponent<MonsterCard>();

                var monster1Level = monster1.GetMonsterLevel();
                var monster2Level = monster2.GetMonsterLevel();

                //Level
                if(monster1Level != monster2Level){
                    Debug.Log("Fusion Failed - Levels are not equal");
                    StopCoroutine(_fusionRoutine);
                }

                if(monster1Level == monster2Level){
                    var monster1Atk = monster1.GetMonsterAtk();
                    var monster2Atk = monster2.GetMonsterAtk();

                    EMonsterType strongestMonsterType;
                    if(monster2Atk < monster1Atk){
                        strongestMonsterType = monster1.GetMonsterType();
                    }else{
                        strongestMonsterType = monster2.GetMonsterType();
                    }

                    List<MonsterCardSO> strongestMonsterList = new();
                    switch(strongestMonsterType){
                        case EMonsterType.Angel:
                            strongestMonsterList = CardsDatabase.Instance.GetAngels();
                        break;
                        case EMonsterType.Machine:
                            strongestMonsterList = CardsDatabase.Instance.GetMachines();
                        break;
                        case EMonsterType.Dragon:
                            strongestMonsterList = CardsDatabase.Instance.GetDragons();
                        break;
                        default:
                            Debug.Log("Error getting the strongest monster list");
                        break;
                    }

                    List<MonsterCardSO> possibleMonsterList = new();
                    foreach(var monster in strongestMonsterList){
                        if(monster.Level == monster1Level +1){
                            possibleMonsterList.Add(monster);
                        }
                    }

                    //Fusion Success
                    var randomIndex = UnityEngine.Random.Range(0,possibleMonsterList.Count);
                    // InstantiateCard(CardCreator.Instance.CreateCard(_deck.GetDeckInUse()[randomIndex]));
                    OnFusionEnded?.Invoke();
                }
            }
        }

        private void MoveSelectedCardsToPosition(List<Card> selectedCards){
            var cardIndex = 0;

            foreach(var card in selectedCards){
                var cardCollider = card.GetComponent<Collider>();
                cardCollider.enabled = false;

                if(cardIndex == 0){
                    card.transform.SetParent(_card1InLinePosition.transform);
                    card.MoveCard(_card1InLinePosition.position, _card1InLinePosition.rotation);

                }else if(cardIndex == 1){
                    card.transform.SetParent(_card2InLinePosition.transform);
                    card.MoveCard(_card2InLinePosition.position, _card2InLinePosition.rotation);

                }else{
                    card.transform.SetParent(_card2InLinePosition.transform);
                    var offsetPosition = 0.3f * cardIndex;
                    Vector3 finalPosition = new((float)(_card2InLinePosition.position.x + offsetPosition), _card2InLinePosition.position.y, _card2InLinePosition.position.z);
                    card.MoveCard(finalPosition, _card2InLinePosition.rotation);
                }

                cardIndex++;
            }
        }
    }
}