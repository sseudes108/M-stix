using System.Collections.Generic;
using UnityEngine;

public class CardsDatabase : MonoBehaviour{
    public static CardsDatabase Instance;

    public List<CardSO> Angels => _angelList;
    public List<CardSO> Dragons => _dragonList;
    public List<CardSO> Machines => _machineList;


    [SerializeField] private List<CardSO> _angelList;
    [SerializeField] private List<CardSO> _dragonList;
    [SerializeField] private List<CardSO> _machineList;

    private void Awake() {
        if(Instance != null){
            Debug.Log("Error: There's more than one instance of CardsDatabase" + Instance + this);
            Destroy(gameObject);
        }
        Instance = this;
    }
}
