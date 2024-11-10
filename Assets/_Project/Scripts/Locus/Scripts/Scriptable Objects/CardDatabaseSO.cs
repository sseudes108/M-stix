using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabaseSO", menuName = "Mistix/Card/Database")]
public class CardDatabaseSO : ScriptableObject {
    [SerializeField] private List<MonsterCardSO> _angels;
    private List<MonsterCardSO> _monsterList = new();
    
    public List<MonsterCardSO> GetStrongestTypeList(EMonsterType type){
        _monsterList = null;
        switch(type){
            case EMonsterType.Angel:
                _monsterList = _angels;
            break;
        }
        return _monsterList;
    }
}