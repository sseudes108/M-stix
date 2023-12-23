
using System.Collections.Generic;
using UnityEngine;

public class CardsDatabase : MonoBehaviour{
    public static CardsDatabase Instance;
    public List<CardSO> DragonList => _dragonList;
    public List<CardSO> AngelList => _angelList;

    [SerializeField] private List<CardSO> _dragonList;
    [SerializeField] private List<CardSO> _angelList;

    private void Awake() {
        if(Instance == null){Instance = this;}
    }
}
