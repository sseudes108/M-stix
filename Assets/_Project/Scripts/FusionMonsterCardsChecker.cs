using System.Collections.Generic;
using UnityEngine;

public class FusionMonsterCardsChecker : MonoBehaviour{
    private Monster.MonsterType _typeOfTheStrongestMonster;
    private List<CardSO> _listOfMonstersOfTheStrongestType;
    private List<CardSO> _listOfPossibleMonsters;
    private int _fusionLevel;

    public List<CardSO> CheckCardsToFusion(MonsterCard monster1, MonsterCard monster2){
        CheckMonstersLevels(monster1, monster2);
        _typeOfTheStrongestMonster = GetStrongestMonsterType(monster1, monster2);
        _listOfMonstersOfTheStrongestType = GetListOfMonstersOfTheStrongestType(_typeOfTheStrongestMonster);
        _listOfPossibleMonsters = GetListOfPossibleMonsters(_listOfMonstersOfTheStrongestType);

        return _listOfPossibleMonsters;
    }

    private void CheckMonstersLevels(MonsterCard monster1, MonsterCard monster2){
        if(monster1.MonsterInfo.Level != monster2.MonsterInfo.Level){
            Debug.Log($"Lvls are not equal! Monster1:{monster1.MonsterInfo.Level}, Monster2:{monster2.MonsterInfo.Level}");
            return;
        }else{
            _fusionLevel = monster2.MonsterInfo.Level + 1;
        }
    }

    private Monster.MonsterType GetStrongestMonsterType(MonsterCard monster1, MonsterCard monster2){
        Monster.MonsterType strongesTypeMonster;
        int atkMonster1 = monster1.MonsterInfo.Atk;
        int atkMonster2 = monster2.MonsterInfo.Atk;
        if (atkMonster1 > atkMonster2){
            strongesTypeMonster = monster1.MonsterInfo.Type;
        }
        else{
            strongesTypeMonster = monster2.MonsterInfo.Type;
        }
        return strongesTypeMonster;
    }

    private List<CardSO> GetListOfMonstersOfTheStrongestType(Monster.MonsterType typeOfTheStrongestMonster){
        switch (typeOfTheStrongestMonster){
            case Monster.MonsterType.Angel:
                return CardsDatabase.Instance.Angels;
            case Monster.MonsterType.Dragon:
                return CardsDatabase.Instance.Dragons;
            case Monster.MonsterType.Machine:
                return CardsDatabase.Instance.Machines;
            default:
                return null;
        }
    }

    private List<CardSO> GetListOfPossibleMonsters(List<CardSO> listOfMonstersOfTheStrongestType){
        List<CardSO> possibleMonsters = new();
        foreach(var monster in listOfMonstersOfTheStrongestType){
            if(monster.GetMonsterInfo().Level == _fusionLevel){
                possibleMonsters.Add(monster);
            }
        }
        return possibleMonsters;
    }
}
