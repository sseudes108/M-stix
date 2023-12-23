
using System.Collections.Generic;
using UnityEngine;

public class AllCards : MonoBehaviour{
    public static AllCards Instance;

    public List<MonsterSO> DragonList => _dragonList;
    public List<MonsterSO> AngelList => _angelList;

    [SerializeField] private List<MonsterSO> _dragonList;
    [SerializeField] private List<MonsterSO> _angelList;

    private void Awake() {
        if(Instance == null){Instance = this;}
    }
}
