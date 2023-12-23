using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour{

    [SerializeField] private Card _cardPrefab;

    [SerializeField] private Card _monster1, _monster2;

    [SerializeField] private List<CardSO> _possibleMonsters;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
            StartCoroutine(FusionRoutine(_monster1, _monster2));
        }
    }

    private IEnumerator FusionRoutine(Card monster1, Card monster2){
    
        if(!isLvlEqual(monster1, monster2)){
            Debug.Log(string.Format("lvls are not equals! Monster1: {0}, Monsters2:{1}", monster1.MonsterInfo.LVL, monster2.MonsterInfo.LVL));
            Destroy(monster1.gameObject);
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(1);

        //Verifica o monstro com maior ataque e assim o tipo de monstro a ser instanciado
        CardSO.MonsterType strongestMonsterType = TypeOfStrongestMonster(monster1, monster2);
        Debug.Log(string.Format("Type of the strongest: {0}", strongestMonsterType));
        yield return new WaitForSeconds(1);
        
        //Pega a lista de monstros do tipo informado
        List<CardSO> listOfMonstersByType = GetListOfMonsters(strongestMonsterType);
        
        //Cria uma nova lista com os monstros do tipo informado com um lvl mais forte que o monstro 2
        List<CardSO> possibleMonsters = MonstersWithHigherLvl(listOfMonstersByType, monster2.MonsterInfo.LVL);
        _possibleMonsters = possibleMonsters;
        
        //Cria o Controller
        int randomIndex = RandomValue(possibleMonsters);
        Debug.Log(string.Format("Picked monster: {0}, {1}", possibleMonsters[randomIndex].name, possibleMonsters[randomIndex].MonsterInfo.Name));

        //Define a data da lista de monstros possiveis criada
        _cardPrefab.SetCardData(possibleMonsters[randomIndex]);
        Debug.Log("Last step");
        yield return new WaitForSeconds(1);
        
        //Instancia
        Instantiate(_cardPrefab, monster1.transform.position, monster1.transform.rotation);

        Destroy(monster1.gameObject);
        Destroy(monster2.gameObject);
    }

    private int RandomValue(List<CardSO> list){
        return Random.Range(0, list.Count);
    }

    private bool isLvlEqual(Card monster1, Card monster2){
        if(monster1.MonsterInfo.LVL == monster2.MonsterInfo.LVL){
            return true;
        }else{
            return false;
        }
    }

    //Verifica o tipo do monstro mais forte
    private CardSO.MonsterType TypeOfStrongestMonster(Card monster1, Card monster2){
        if(monster2.MonsterInfo.ATK >= monster1.MonsterInfo.ATK){
            return monster2.MonsterInfo.Type;
        }else{
            return monster1.MonsterInfo.Type;
        }
    }

    //Lista de monstros do mesmo tipo do monstro mais forte
    private List<CardSO> GetListOfMonsters(CardSO.MonsterType type){
        if(type == CardSO.MonsterType.Dragon){
            return AllCards.Instance.DragonList;
        }else{
            return AllCards.Instance.AngelList;
        }
    }

    //Monstros possiveis (Lista de monstros com 1 nivel superior do tipo do monstro mais forte da fusão)
    private List<CardSO> MonstersWithHigherLvl(List<CardSO> list, int lvl){
        List<CardSO> possibleMonsters = new List<CardSO>();
        foreach (CardSO monster in list){
            if(monster.MonsterInfo.LVL > lvl){
                possibleMonsters.Add(monster);
            }
        }
        return possibleMonsters;
    }
}