using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour {
    [field:SerializeField] public List<MonsterCardSO> Angels { get; private set; }
    
    private void OnEnable() {
        MonsterFusion.OnCheckCardsBase += MonstersFusion_OnCheckCardsBase;
    }

    private void OnDisable() {
        MonsterFusion.OnCheckCardsBase -= MonstersFusion_OnCheckCardsBase;
    }

    private void MonstersFusion_OnCheckCardsBase(MonsterFusion fusion, EMonsterType type){
        var list = new List<MonsterCardSO>();
        switch(type){
            case EMonsterType.Angel:
                list = Angels;
            break;
        }
        fusion.SetStrongestTypeList(list);
    }

    
}