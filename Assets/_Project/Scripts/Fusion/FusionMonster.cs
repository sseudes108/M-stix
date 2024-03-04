using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionMonster : Fusion {

    public void MonsterFusion(CardMonster monster1, CardMonster monster2){
        StartCoroutine(StartMonsterFusionRoutine(monster1, monster2));
    }

    private IEnumerator StartMonsterFusionRoutine(CardMonster monster1, CardMonster monster2){

        var monster1Lvl = monster1.GetLevel();
        var monster2Lvl = monster2.GetLevel();

        var monster1Atk = monster1.GetAttack();
        var monster2Atk = monster2.GetAttack();

        //Fusion Failed
        bool equalLevels = true;
        if(monster1Lvl != monster2Lvl){
            //Not equal levels
            #region Fusion Failed
                Debug.Log("Fusion Failed - Lvls are not equals");
                //Remove Cards From line
                BattleManager.Instance.Fusion.RemoveCardsFromFusionLine(monster1, monster2);

                //Dissolve the first card
                BattleManager.Instance.FusionVisuals.DissolveCard(monster1, Color.red);
                yield return new WaitForSeconds(0.1f);

                //Move the second card position
                BattleManager.Instance.FusionPositions.MoveCardToFirstPositionInlinePos(monster2);
                yield return new WaitForSeconds(1f);

                //Check if the line is 0
                if(BattleManager.Instance.Fusion.GetCardsInFusionLine() > 0){
                    BattleManager.Instance.Fusion.AddCardToFusionLine(monster2);
                }else{
                    BattleManager.Instance.FusionPositions.FusionFailed(monster2);
                }

                Debug.Log("Corrigir! Fusion Failed - FusionMonster - Destroy Card");
                monster1.DisableModelVisual();

            #endregion

            //Block the rest of the routine
            equalLevels = false;
            yield return new WaitForSeconds(3);
        }

        if(equalLevels){
            //Get Strongest Monster Type
            EMonsterType strongestMonsterType;
            if(monster1Atk > monster2Atk){
                strongestMonsterType = monster1.GetMonsterType();
            }else{
                strongestMonsterType = monster2.GetMonsterType();
            }

            //Get List of the strongest Monster Type
            List<CardMonsterSO> strongestTypeList = new();
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
                case EMonsterType.She:
                    strongestTypeList = BattleManager.Instance.CardsDatabase.She;
                break;
                default:
                    Debug.Log("Error. Type not found");
                break;
            }
            Debug.Log($"Stongest Monster Type {strongestMonsterType}");

            //List of the possible monsters (Correct lvl)
            List<CardMonsterSO> possibleMonsters = new();
            foreach(var monster in strongestTypeList){
                if(monster.Level == monster1Lvl + 1){
                    possibleMonsters.Add(monster);
                }
            }

            //Fusion Sucess//

            //Cards used in fusion
            List<Card> materials = new(){monster1,monster2};

            //Move cards
            BattleManager.Instance.FusionPositions.FusionSucess(materials);

            yield return new WaitForSeconds(0.4f);
            //Dissolve cards used
            BattleManager.Instance.FusionVisuals.DissolveCard(materials, Color.green);

            //Instantiate fusioned card
            var randomIndex = Random.Range(0, possibleMonsters.Count);
            var fusionedCard = Instantiate(BattleManager.Instance.CardCreator.CreateCard(possibleMonsters[randomIndex]));
            fusionedCard.name = $"{fusionedCard.GetCardName()} - Fusioned";

            //Set Card Owner
            if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
                fusionedCard.SetPlayerCard();
            }

            //Momentaneo, apenas para testar a visualização da card no UI - 
            //Ativar o colider apenas quando a card for adicionada um board place
                fusionedCard.EnableCollider();
            //

            //make card invisible
            fusionedCard.DisableStatCanvas();
            fusionedCard.DisableModelVisual();
            BattleManager.Instance.FusionVisuals.MakeCardInvisible(fusionedCard);

            //Move fusioned card to position
            fusionedCard.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosition);

            //Make card appear
            yield return new WaitForSeconds(1f);
            BattleManager.Instance.FusionVisuals.SolidifyCard(fusionedCard, Color.white);

            // var teste = BattleManager.Instance.Fusion.GetCardsInFusionLine();
            if(BattleManager.Instance.Fusion.GetCardsInFusionLine() > 0){
                BattleManager.Instance.Fusion.AddCardToFusionLine(fusionedCard);
            }

            //Deactivate objetcs of the used cards (Destroy)
            monster1.gameObject.SetActive(false);
            monster2.gameObject.SetActive(false);
        }
    }
}