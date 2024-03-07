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
            BattleManager.Instance.Fusion.FusionFailed(monster1, monster2);
            
            //Block the rest of the routine
            equalLevels = false;
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

            //List of the possible monsters (Correct lvl)
            List<CardMonsterSO> possibleMonsters = new();
            foreach(var monster in strongestTypeList){
                if(monster.Level == monster1Lvl + 1){
                    possibleMonsters.Add(monster);
                }
            }

            //Fusion Sucess//

            //Instantiate fusioned card
            var randomIndex = Random.Range(0, possibleMonsters.Count);
            var fusionedCard = Instantiate(BattleManager.Instance.CardCreator.CreateCard(possibleMonsters[randomIndex]));
            fusionedCard.name = $"{fusionedCard.GetCardName()} - Fusioned";
            fusionedCard.SetFusionedCard();

            //make card invisible
            fusionedCard.DisableStatCanvas();
            fusionedCard.DisableModelVisual();
            BattleManager.Instance.CardVisuals.MakeCardInvisible(fusionedCard);

            //Fusion 
            BattleManager.Instance.Fusion.FusionSucess(monster1, monster2, fusionedCard);

            //Make card Visible
            yield return new WaitForSeconds(1f);
            BattleManager.Instance.CardVisuals.SolidifyCard(fusionedCard, Color.white);
        }
    }
}