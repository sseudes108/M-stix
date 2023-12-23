using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour{

    [SerializeField] private MonsterController _monsterCardPrefab;

    [SerializeField] private MonsterController _monster1, _monster2;

    [SerializeField] private List<MonsterSO> _possibleMonsters;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
            StartCoroutine(FusionRoutine(_monster1, _monster2));
        }
    }

    private IEnumerator FusionRoutine(MonsterController monster1, MonsterController monster2){
    
        if(!isLvlEqual(monster1, monster2)){
            // destruir monstro 1
            Debug.Log(string.Format("lvls are not equals! Monster1: {0}, Monsters2:{1}", monster1.MonsterInfo.LVL, monster2.MonsterInfo.LVL));
            monster1.gameObject.SetActive(false);
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(1);

        //Verifica o monstro com maior ataque e assim o tipo de monstro a ser instanciado
        MonsterSO.MonsterType strongestMonsterType = TypeOfStrongestMonster(monster1, monster2);
        Debug.Log(string.Format("Type of the strongest: {0}", strongestMonsterType));
        yield return new WaitForSeconds(1);
        
        //Pega a lista de monstros do tipo informado
        List<MonsterSO> listOfMonstersByType = GetListOfMonsters(strongestMonsterType);
        
        //Cria uma nova lista com os monstros do tipo informado com um lvl mais forte que o monstro 2
        List<MonsterSO> possibleMonsters = MonstersWithHigherLvl(listOfMonstersByType, monster2.MonsterInfo.LVL);
        _possibleMonsters = possibleMonsters;
        
        //Cria o Controller
        //MonsterController newMonster = new();
        int randomIndex = RandomValue(possibleMonsters);
        Debug.Log(string.Format("Picked monster: {0}, {1}", possibleMonsters[randomIndex].name, possibleMonsters[randomIndex].MonsterInfo.Name));

        //Define a data da lista de monstros possiveis criada
        _monsterCardPrefab.SetMonsterData(possibleMonsters[randomIndex]);
        Debug.Log("Last step");
        yield return new WaitForSeconds(1);
        
        //Instancia
        Instantiate(_monsterCardPrefab, monster1.transform.position, monster1.transform.rotation);

        monster1.gameObject.SetActive(false);
        monster2.gameObject.SetActive(false);
    }

    private int RandomValue(List<MonsterSO> list){
        return Random.Range(0, list.Count);
    }

    private bool isLvlEqual(MonsterController monster1, MonsterController monster2){
        if(monster1.MonsterInfo.LVL == monster2.MonsterInfo.LVL){
            return true;
        }else{
            return false;
        }
    }

    //Verifica o tipo do monstro mais forte
    private MonsterSO.MonsterType TypeOfStrongestMonster(MonsterController monster1, MonsterController monster2){
        if(monster2.MonsterInfo.ATK >= monster1.MonsterInfo.ATK){
            return monster2.MonsterInfo.Type;
        }else{
            return monster1.MonsterInfo.Type;
        }
    }

    //Lista de monstros do mesmo tipo do monstro mais forte
    private List<MonsterSO> GetListOfMonsters(MonsterSO.MonsterType type){
        if(type == MonsterSO.MonsterType.Dragon){
            return AllCards.Instance.DragonList;
        }else{
            return AllCards.Instance.AngelList;
        }
    }

    //Monstros possiveis (Lista de monstros com 1 nivel superior do tipo do monstro mais forte da fusão)
    private List<MonsterSO> MonstersWithHigherLvl(List<MonsterSO> list, int lvl){
        List<MonsterSO> possibleMonsters = new List<MonsterSO>();
        foreach (MonsterSO monster in list){
            if(monster.MonsterInfo.LVL > lvl){
                possibleMonsters.Add(monster);
            }
        }
        return possibleMonsters;
    }
}
