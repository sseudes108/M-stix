using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionMonster : Fusion {

    private IEnumerator StartMonsterFusionRoutine(CardMonster monster1, CardMonster monster2){
        yield return new WaitForSeconds(1);

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

        //Fusion Sucess//

        //cards used in fusion
        List<Card> materials = new(){monster1,monster2};

        //move cards
        BattleManager.Instance.FusionPositions.FusionSucces_MoveCardMaterials(materials);
        yield return new WaitForSeconds(1 / 3);

        //Dissolve cards used
        BattleManager.Instance.FusionVisuals.DissolveCard(materials);

        //Deactivate objetcs of the used cards (Destroy)
        yield return new WaitForSeconds(2);
        monster1.gameObject.SetActive(false);
        monster2.gameObject.SetActive(false);
        
        //Instantiate fusioned card
        var randomIndex = Random.Range(0, possibleMonsters.Count);
        var fusionedCard = Instantiate(BattleManager.Instance.CardCreator.CreateCard(possibleMonsters[randomIndex]));
        fusionedCard.name = $"{fusionedCard.GetCardName()} - Fusioned";
        
        //move fusioned card to position
        fusionedCard.transform.SetParent(BattleManager.Instance.FusionPositions.ResultCardPosition);
        fusionedCard.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosition.position, 
            BattleManager.Instance.FusionPositions.ResultCardPosition.rotation
        );

        if(BattleManager.Instance.Fusion.CardsInFusionLine() > 0){
            BattleManager.Instance.Fusion.AddCardToFusionLine(fusionedCard);
        }
    }

    public void MonsterFusion(CardMonster monster1, CardMonster monster2){
        StartCoroutine(StartMonsterFusionRoutine(monster1, monster2));
    }
}