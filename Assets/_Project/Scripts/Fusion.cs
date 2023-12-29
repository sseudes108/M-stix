using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour{
    public static Fusion Instance {get; private set;}

    [SerializeField] private MonsterCard _MonsterCardPrefab;

    private void Awake() {
        if(Instance != null){
            Debug.Log("Error: There's more than one instance of Fusion" + Instance + this);
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void FusionCards(List<MonsterCard> cards){
        StartCoroutine(FusionCardsRoutine(cards));
    }

    private IEnumerator FusionCardsRoutine(List<MonsterCard> cardToFusion)
    {
        MonsterCard monster1 = cardToFusion[0];
        MonsterCard monster2 = cardToFusion[1];

        Monster.MonsterType typeOfTheStrongestMonster = GetStrongestMonsterType(monster1, monster2);
        int strongestAttack = GetAtkOfTheStrongestMonster(monster1,monster2);
        List<CardSO> listOfMonstersOfTheStrongestType = GetListOfMonstersOfTheStrongestType(typeOfTheStrongestMonster);
        List<CardSO> listOfPossibleMonsters = GetListOfPossibleMonsters(listOfMonstersOfTheStrongestType, strongestAttack);

        FusionMonster(monster1, listOfPossibleMonsters);

        yield return new WaitForSeconds(0.2f);

        monster1.gameObject.SetActive(false);
        monster2.gameObject.SetActive(false);
    }

    private void FusionMonster(MonsterCard monster1, List<CardSO> listOfPossibleMonsters){
        int randomIndex = Random.Range(0, listOfPossibleMonsters.Count);
        MonsterCard newMonsterCard = _MonsterCardPrefab;
        newMonsterCard.SetMonsterData(listOfPossibleMonsters[randomIndex]);
        Instantiate(newMonsterCard, monster1.transform.position, monster1.transform.rotation);
    }

    private int GetAtkOfTheStrongestMonster(MonsterCard monster1, MonsterCard monster2){
        int atkMonster1 = monster1.MonsterInfo.Atk;
        int atkMonster2 = monster2.MonsterInfo.Atk;
        if (atkMonster1 > atkMonster2){
            return atkMonster1;
        }
        else{
            return atkMonster2;
        }
    }

    private List<CardSO> GetListOfPossibleMonsters(List<CardSO> listOfMonstersOfTheStrongestType, int strongestAttack){
        List<CardSO> possibleMonsters = new();

        foreach(var monster in listOfMonstersOfTheStrongestType){
            if(monster.GetMonsterInfo().Atk > strongestAttack){
                possibleMonsters.Add(monster);
                Debug.Log(monster);
            }
        }
        return possibleMonsters;
    }

    private List<CardSO> GetListOfMonstersOfTheStrongestType(Monster.MonsterType typeOfTheStrongestMonster)
    {
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
}
