using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionCardsChecker : MonoBehaviour{
    public static FusionCardsChecker Instance {get; private set;}
    private Card _resultCard;
    
    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one FusionMonsterCardsChecker instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;
    }

    public void StartCheck(List<Card> selectedCards){
        StartCoroutine(CheckCards(selectedCards));
    }
    
    private IEnumerator CheckCards(List<Card> selectedCards){
        Debug.Log("CheckCards");

        Card card1 = selectedCards[0];
        Card card2 = selectedCards[1];

        if(card1 is ArcaneCard){
            // Debug.Log("card1 is ArcaneCard");

            FusionFailed(card1, card2);
            yield return new WaitForSeconds(1f);
        }

        if(card1 is MonsterCard){
            // Debug.Log("card1 is MonsterCard");
            if(card2 is ArcaneCard){
                // Debug.Log("card2 is ArcaneCard");

                FusionFailed(card1, card2);
                yield return new WaitForSeconds(1f);
            }else{
                // Debug.Log("card1 is MonsterCard");
                // Debug.Log("card2 is MonsterCard");
                MonsterCard monsterCard1 = card1.GetComponent<MonsterCard>();
                MonsterCard monsterCard2 = card2.GetComponent<MonsterCard>();

                int lvlMonster1 = monsterCard1.GetLevel();
                int lvlMonster2 = monsterCard2.GetLevel();

                if(lvlMonster1 != lvlMonster2){
                    // Debug.Log("lvlMonster1 != lvlMonster2");

                    FusionFailed(card1, card2);
                    yield return new WaitForSeconds(1f);
                }

                if(lvlMonster1 == lvlMonster2){
                    // Debug.Log("lvlMonster1: " + lvlMonster1 + "==" + "lvlMonster2: " + lvlMonster2);
                    yield return new WaitForSeconds(0.5f);

                    MonsterCardSO.MonsterType strongestMonsterType = GetStrongestMonsterType(monsterCard1, monsterCard2);
                    List<MonsterCardSO> listOfStrongestType = GetListOfMonstersOfTheStrongestType(strongestMonsterType);
                    List<MonsterCardSO> possibleMonsters = GetListOfPossibleMonsters(monsterCard1, listOfStrongestType);

                    CreateFusionedCard(possibleMonsters);

                    CardSelector.Instance.RemoveCardFromSelectedList(card1);
                    CardSelector.Instance.RemoveCardFromSelectedList(card2);

                    Destroy(card1.gameObject);
                    Destroy(card2.gameObject);

                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    private void FusionFailed(Card card1, Card card2){
        card2.transform.position = card1.transform.position;
        CardSelector.Instance.RemoveCardFromSelectedList(card1);
        CardSelector.Instance.RemoveCardFromSelectedList(card2);

        _resultCard = card2;

        Destroy(card1.gameObject);
        StopAllCoroutines();
    }

    private static List<MonsterCardSO> GetListOfPossibleMonsters(MonsterCard monsterCard1, List<MonsterCardSO> listOfStrongestType){
        List<MonsterCardSO> possibleMonsters = new();
        int fusionLevel = monsterCard1.GetLevel() + 1;

        foreach (MonsterCardSO monster in listOfStrongestType){
            int monsterLevel = monster.Level;
            if (monsterLevel == fusionLevel){
                possibleMonsters.Add(monster);
            }
        }

        return possibleMonsters;
    }
    private static MonsterCardSO.MonsterType GetStrongestMonsterType(MonsterCard monsterCard1, MonsterCard monsterCard2){
        int AtkMonster1 = monsterCard1.GetAtk();
        int AtkMonster2 = monsterCard2.GetAtk();

        MonsterCardSO.MonsterType strongestMonsterType;
        if (AtkMonster1 <= AtkMonster2){
            strongestMonsterType = monsterCard2.GetMonsterType();
        }
        else{
            strongestMonsterType = monsterCard1.GetMonsterType();
        }
        return strongestMonsterType;
    }
    private static List<MonsterCardSO> GetListOfMonstersOfTheStrongestType(MonsterCardSO.MonsterType strongestMonsterType){
        List<MonsterCardSO> listOfStrongestType;
        switch (strongestMonsterType){
            case MonsterCardSO.MonsterType.Angel:
                listOfStrongestType = CardsDatabase.Instance.Angels;
                break;
            case MonsterCardSO.MonsterType.Dragon:
                listOfStrongestType = CardsDatabase.Instance.Dragons;
                break;
            case MonsterCardSO.MonsterType.Machine:
                listOfStrongestType = CardsDatabase.Instance.Machines;
                break;
            case MonsterCardSO.MonsterType.Golem:
                listOfStrongestType = CardsDatabase.Instance.Golens;
                break;
            case MonsterCardSO.MonsterType.Witch:
                listOfStrongestType = CardsDatabase.Instance.Witches;
                break;
            case MonsterCardSO.MonsterType.Alchemist:
                listOfStrongestType = CardsDatabase.Instance.Alchemists;
                break;
            case MonsterCardSO.MonsterType.Beast:
                listOfStrongestType = CardsDatabase.Instance.Beasts;
                break;
            case MonsterCardSO.MonsterType.Demon:
                listOfStrongestType = CardsDatabase.Instance.Demons;
                break;
            default:
                Debug.Log("Error! Type not Identified " + strongestMonsterType);
                listOfStrongestType = null;
                break;
        }
        return listOfStrongestType;
    }

    private void CreateFusionedCard(List<MonsterCardSO> possibleMonsters){
        int randomIndex = Random.Range(0, possibleMonsters.Count);
        _resultCard = CardCreator.Instance.CreateCard(possibleMonsters[randomIndex]);
    }

    public Card GetResultCard(){
        return _resultCard;
    }
}