using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionCardsChecker : MonoBehaviour{
    public static FusionCardsChecker Instance {get; private set;}
    private Card _fusionedCard;
    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one FusionMonsterCardsChecker instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;
    }

    public void StartCheck(List<Card> selectedCards){
        StartCoroutine(CheckCards(selectedCards));
    }
    private IEnumerator CheckCards(List<Card> selectedCards){
        // DisableCardsCollider(selectedCards);

        Card card1 = selectedCards[0];
        Card card2 = selectedCards[1];

        if(card1 is ArcaneCard){
            if(card2 is ArcaneCard){
                card2.transform.position = card1.transform.position;               
                CardSelector.Instance.RemoveCardFromSelectedList(card1);
                //card1.gameObject.SetActive(false);
                Destroy(card1.gameObject);
            }
        }

        if(card1 is MonsterCard){
            if(card2 is ArcaneCard){
                card2.transform.position = card1.transform.position;
                CardSelector.Instance.RemoveCardFromSelectedList(card1);
                //card1.gameObject.SetActive(false);
                Destroy(card1.gameObject);
            }else{
                MonsterCard monsterCard1 = card1.GetComponent<MonsterCard>();
                MonsterCard monsterCard2 = card2.GetComponent<MonsterCard>();

                int lvlMonster1 = monsterCard1.GetLevel();
                int lvlMonster2 = monsterCard2.GetLevel();

                if(lvlMonster1 != lvlMonster2){
                    card2.transform.position = card1.transform.position;
                    //card1.gameObject.SetActive(false);
                    Destroy(card1.gameObject);
                }

                if(lvlMonster1 == lvlMonster2){
                    yield return new WaitForSeconds(0.5f);

                    MonsterCardSO.MonsterType strongestMonsterType = GetStrongestMonsterType(monsterCard1, monsterCard2);
                    List<MonsterCardSO> listOfStrongestType = GetListOfMonstersOfTheStrongestType(strongestMonsterType);
                    List<MonsterCardSO> possibleMonsters = GetListOfPossibleMonsters(monsterCard1, listOfStrongestType);

                    CreateFusionedCard(possibleMonsters);

                    yield return new WaitForSeconds(0.5f);

                    CardSelector.Instance.RemoveCardFromSelectedList(card1);
                    //card1.gameObject.SetActive(false);
                    Destroy(card1.gameObject);

                    CardSelector.Instance.RemoveCardFromSelectedList(card2);
                    //card1.gameObject.SetActive(false);
                    Destroy(card2.gameObject);
                }
            }
        }
    }

    private static List<MonsterCardSO> GetListOfPossibleMonsters(MonsterCard monsterCard1, List<MonsterCardSO> listOfStrongestType)
    {
        List<MonsterCardSO> possibleMonsters = new();
        int fusionLevel = monsterCard1.GetLevel() + 1;

        foreach (MonsterCardSO monster in listOfStrongestType)
        {
            int monsterLevel = monster.Level;
            if (monsterLevel == fusionLevel)
            {
                possibleMonsters.Add(monster);
            }
        }

        return possibleMonsters;
    }

    private static MonsterCardSO.MonsterType GetStrongestMonsterType(MonsterCard monsterCard1, MonsterCard monsterCard2)
    {
        int AtkMonster1 = monsterCard1.GetAtk();
        int AtkMonster2 = monsterCard2.GetAtk();

        MonsterCardSO.MonsterType strongestMonsterType;
        if (AtkMonster1 <= AtkMonster2)
        {
            strongestMonsterType = monsterCard2.GetMonsterType();
        }
        else
        {
            strongestMonsterType = monsterCard1.GetMonsterType();
        }

        return strongestMonsterType;
    }

    private static List<MonsterCardSO> GetListOfMonstersOfTheStrongestType(MonsterCardSO.MonsterType strongestMonsterType)
    {
        List<MonsterCardSO> listOfStrongestType;
        switch (strongestMonsterType)
        {
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
                listOfStrongestType = null;
                break;
        }

        return listOfStrongestType;
    }

    private void CreateFusionedCard(List<MonsterCardSO> possibleMonsters)
    {
        int randomIndex = Random.Range(0, possibleMonsters.Count);
        _fusionedCard = CardCreator.Instance.CreateCard(possibleMonsters[randomIndex]);
    }

    public Card FusionedCard(){
        return _fusionedCard;
    }
}
