using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabaseSO", menuName = "Mistix/Card/Database")]
public class CardDatabaseSO : ScriptableObject {
    [SerializeField] private List<MonsterCardSO> _angels;

    public List<MonsterCardSO> GetStrongestTypeList(EMonsterType type){
        var list = new List<MonsterCardSO>();
        switch(type){
            case EMonsterType.Angel:
                list = _angels;
            break;
        }
        return list;
    }
}