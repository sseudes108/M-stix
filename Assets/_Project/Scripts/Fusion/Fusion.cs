using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour{
    public static Fusion Instance {get; private set;}

    [SerializeField] private MonsterCard _MonsterCardPrefab;
    private FusionMonsterCardsChecker _fusionMonsterCardschecker;
    private FusionListOrganizer _fusionListOrganizer;
    private CardCreator _cardCreator;

    private void Awake() {
        if(Instance != null){
            Debug.Log("Error: There's more than one instance of Fusion" + Instance + this);
            Destroy(gameObject);
        }
        Instance = this;

        _fusionMonsterCardschecker = GetComponent<FusionMonsterCardsChecker>();
        _fusionListOrganizer = GetComponent<FusionListOrganizer>();
        _cardCreator = CardsDatabase.Instance.GetComponent<CardCreator>();
    }

    public void StartFusionLine(List<Card> cards){
        _fusionListOrganizer.CheckCardTypes(cards);
    }

    public void StartMonsterFusion(List<MonsterCard> monstersToFusion){
        StartCoroutine(FusionMonstersRoutine(monstersToFusion));
    }

    private IEnumerator FusionMonstersRoutine(List<MonsterCard> cardToFusion){
        MonsterCard monster1 = cardToFusion[0];
        MonsterCard monster2 = cardToFusion[1];

        List<CardSO> possibleMonsters = new();
        possibleMonsters = _fusionMonsterCardschecker.CheckCardsToFusion(monster1, monster2);

        FusionMonster(monster1, possibleMonsters);

        yield return new WaitForSeconds(0.2f);

        monster1.gameObject.SetActive(false);
        monster2.gameObject.SetActive(false);
        // _fusionListOrganizer.RemoveCardFromList(monster1.gameObject.GetComponent<Card>());
        // _fusionListOrganizer.RemoveCardFromList(monster2.gameObject.GetComponent<Card>());
    }

    private void FusionMonster(MonsterCard monster1, List<CardSO> listOfPossibleMonsters){
        int randomIndex = Random.Range(0, listOfPossibleMonsters.Count);
        MonsterCard newMonsterCard = _MonsterCardPrefab;
        newMonsterCard.SetData(listOfPossibleMonsters[randomIndex]);
        Instantiate(newMonsterCard, monster1.transform.position, monster1.transform.rotation);
    }
}
