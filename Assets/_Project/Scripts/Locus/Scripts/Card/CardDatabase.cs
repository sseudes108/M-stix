using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour {
    public FusionEventHandlerSO FusionManager;
    
    [field:SerializeField] public List<MonsterCardSO> Angels { get; private set; }
    
    private void OnEnable() {
        FusionManager.OnCheckCardsBase.AddListener(FusionManager_OnCheckCardsBase);
    }

    private void OnDisable() {
        FusionManager.OnCheckCardsBase.RemoveListener(FusionManager_OnCheckCardsBase);
    }

    private void FusionManager_OnCheckCardsBase(MonsterFusion fusion, EMonsterType type){
        var list = new List<MonsterCardSO>();
        switch(type){
            case EMonsterType.Angel:
                list = Angels;
            break;
        }
        fusion.SetStrongestTypeList(list);
    }

    
}