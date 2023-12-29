using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour{
    public static Fusion Instance {get; private set;}

    [SerializeField] private MonsterCard _MonsterCardPrefab;
    private FusionMonsterCardsChecker _fusionMonsterCardschecker;
    private FusionListOrganizer _fusionListOrganizer;

    private void Awake() {
        if(Instance != null){
            Debug.Log("Error: There's more than one instance of Fusion" + Instance + this);
            Destroy(gameObject);
        }
        Instance = this;

        _fusionMonsterCardschecker = GetComponent<FusionMonsterCardsChecker>();
        _fusionListOrganizer = GetComponent<FusionListOrganizer>();
    }

    public void StartFusionLine(List<Card> cards){
        _fusionListOrganizer.CheckCardTypes(cards);
    }

    public void StartMonsterFusion(List<MonsterCard> monstersToFusion){
        StartCoroutine(FusionCardsRoutine(monstersToFusion));
    }

    private IEnumerator FusionCardsRoutine(List<MonsterCard> cardToFusion){
        MonsterCard monster1 = cardToFusion[0];
        MonsterCard monster2 = cardToFusion[1];

        List<CardSO> possibleMonsters = new();
        possibleMonsters = _fusionMonsterCardschecker.CheckCardsToFusion(monster1, monster2);

        FusionMonster(monster1, possibleMonsters);

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
}
