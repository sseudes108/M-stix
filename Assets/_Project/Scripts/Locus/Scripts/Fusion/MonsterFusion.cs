using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFusion : Fusion {
    public static Action<MonsterFusion, EMonsterType> OnCheckCardsBase;
    public List<MonsterCardSO> _strongestTypeList = new();

    private void OnEnable() {
        OnMonsterFusion += Fusion_OnMonsterFusion;
    }

    private void OnDisable() {
        OnMonsterFusion += Fusion_OnMonsterFusion;
    }

    private void Fusion_OnMonsterFusion(MonsterCard monster1, MonsterCard monster2){
        if(this != null){
            StartCoroutine(MonsterFusionRoutine(monster1, monster2));
        }else{
            Debug.Log($"Destroy MonsterFusion Instance ID <color=yellow>{this.GetInstanceID()}</color=yellow>");
            Destroy(this);
        }
    }

    public IEnumerator MonsterFusionRoutine(MonsterCard monster1, MonsterCard monster2){
        var monster1Lvl = monster1.Level;
        var monster2Lvl = monster2.Level;

        var monster1Atk = monster1.Attack;
        var monster2Atk = monster2.Attack;

        yield return null;

        //Fusion Failed
        // bool equalLevels = true;
        if(monster1Lvl != monster2Lvl){
            Debug.Log("Fusion Failed - Lvls are not equals");
            //Not equal levels
            OnFusionFailed?.Invoke(monster1, monster2);
            yield break;
        }

        //Fusion Sucess//
        //Get Strongest Monster Type
        EMonsterType strongestMonsterType;
        if(monster1Atk > monster2Atk){
            strongestMonsterType = monster1.MonsterType;
        }else{
            strongestMonsterType = monster2.MonsterType;
        }
        yield return null;

        //List of the possible monsters (Correct lvl)
        OnCheckCardsBase?.Invoke(this, strongestMonsterType);
        yield return null;

        var possibleMonsters = SetPossibleMonstersList(monster1Lvl);

        //Instantiate fusioned card
        var randomIndex = UnityEngine.Random.Range(0, possibleMonsters.Count - 1);
        var fusionedCard = Instantiate(GameManager.Instance.CardManager.CardCreator.CreateCard(possibleMonsters[randomIndex]));
        fusionedCard.name = $"{fusionedCard.Name} - Fusioned";
        fusionedCard.SetFusionedCard();

        //make card invisible
        fusionedCard.DisableModelVisual();

        //Fusion
        OnFusionSucess?.Invoke(monster1, monster2, fusionedCard);

        //Make card Visible
        yield return new WaitForSeconds(2f);
        fusionedCard.EnableModelVisual();
        fusionedCard.CardVisual.Shader.SolidifyCard(Color.white);
        
        yield return null;
    }
    
    public void SetStrongestTypeList(List<MonsterCardSO> strongestMonsterList){
        _strongestTypeList = strongestMonsterList;
    }

    private List<MonsterCardSO> SetPossibleMonstersList(int monsterLvl){
        List<MonsterCardSO> possibleMonsters = new();

        foreach(var monster in _strongestTypeList){
            if(monster.Level == monsterLvl + 1){
                possibleMonsters.Add(monster);
            }
        }

        return possibleMonsters;
    }
}