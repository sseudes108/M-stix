using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {

    public Action OnFusionStart, OnFusionEnd;

    public void StartFusionRoutine(List<Card> selectedCards){
        StartCoroutine(FusionRoutine(selectedCards));
    }

    private IEnumerator FusionRoutine(List<Card> selectedCards){
        float waitTime = 1f;

        //Move hand off camera
        OnFusionStart?.Invoke();

        //Disable Card Colliders
        DisableCardColliders(selectedCards);

        //Reset Colors
        BattleManager.Instance.FusionVisuals.ResetBorderColors(selectedCards);

        //Move cards to fusion line
        BattleManager.Instance.FusionPositions.MoveCardsToPosition(selectedCards);

        var card1 = selectedCards[0];
        var card2 = selectedCards[1];

        //Precisa ser arrumado! da forma que está não é póssivel usar cartas de equipa na linha de fusão.
        if(card1.GetCardType() != card2.GetCardType()){
            Debug.Log("Fusion Failed. Diferent card types");
            // StopAllCoroutines();
        }

        if(card1.GetCardType() == card2.GetCardType()){
            yield return new WaitForSeconds(waitTime);

            if(card1.GetCardType() == ECardType.Monster){
                //FusionMonster

                //Variables
                var monster1 = card1 as CardMonster;
                var monster2 = card2 as CardMonster;

                var monster1Lvl = monster1.GetLevel();
                var monster2Lvl = monster2.GetLevel();

                var monster1Atk = monster1.GetAttack();
                var monster2Atk = monster2.GetAttack();

                yield return new WaitForSeconds(1);

                //Fusion Failed
                if(monster1Lvl != monster2Lvl){
                    Debug.Log("Fusion Failed. Levels are not equals");
                    StopAllCoroutines();
                }

                //Get Strongest Monster Type

                EMonsterType strongestMonsterType;
                if(monster1Atk > monster2Atk){
                    strongestMonsterType = monster1.GetMonsterType();
                }else{
                    strongestMonsterType = monster2.GetMonsterType();
                }

                //Get List of the strongest Monster Type
                List<CardMonsterSO> strongestTypeList = new();

                yield return new WaitForSeconds(waitTime);

                switch(strongestMonsterType){
                    case EMonsterType.Angel:
                        strongestTypeList = BattleManager.Instance.CardsDatabase.Angels;
                    break;
                    case EMonsterType.Machina:
                        strongestTypeList = BattleManager.Instance.CardsDatabase.Machinas;
                    break;
                    case EMonsterType.Dragon:
                        strongestTypeList = BattleManager.Instance.CardsDatabase.Dragons;
                    break;
                    case EMonsterType.Golem:
                        strongestTypeList = BattleManager.Instance.CardsDatabase.Golens;
                    break;
                    default:
                        Debug.Log("Error. Type not found");
                    break;
                }

                //List of the possible monsters (Correct lvl)
                List<CardMonsterSO> possibleMonsters = new();
                var targetLvl = monster1Lvl + 1;

                foreach(var monster in strongestTypeList){
                    if(monster.Level == targetLvl){
                        possibleMonsters.Add(monster);
                    }
                }

                List<Card> materials = new(){card1,card2};
                BattleManager.Instance.FusionPositions.FusionSucces_MoveCardMaterials(materials);
                yield return new WaitForSeconds(waitTime / 3);
                BattleManager.Instance.FusionVisuals.DissolveCards(materials);

                yield return new WaitForSeconds(waitTime + 1);
                card1.gameObject.SetActive(false);
                card2.gameObject.SetActive(false);

                var randomIndex = UnityEngine.Random.Range(0, possibleMonsters.Count);
                var fusionedCard = Instantiate(BattleManager.Instance.CardCreator.CreateCard(possibleMonsters[randomIndex]));
                fusionedCard.name = $"{fusionedCard.GetCardName()} - Fusioned";
                
                fusionedCard.transform.SetParent(BattleManager.Instance.FusionPositions.ResultCardPosition);
                fusionedCard.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosition.position, 
                    BattleManager.Instance.FusionPositions.ResultCardPosition.rotation
                );

            }else if(card1.GetCardType() == ECardType.Arcane){
                //FusionArcane
            }
        }

        yield return new WaitForSeconds(waitTime);
        OnFusionEnd?.Invoke();
        Debug.Log("Fusion Ended");
        // Debug.Log("Coroutine did not stopped");
    }

    private void DisableCardColliders(List<Card> selectedCards){
        foreach(var card in selectedCards){
            card.DisableCardCollider();
        }
    }
}