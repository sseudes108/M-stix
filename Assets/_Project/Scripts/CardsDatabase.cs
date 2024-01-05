using System.Collections.Generic;
using UnityEngine;

public class CardsDatabase : MonoBehaviour{
    public static CardsDatabase Instance {get; private set;}
    public List<MonsterCardSO> Angels => _angelList;
    public List<MonsterCardSO> Dragons => _dragonList;
    public List<MonsterCardSO> Machines => _machineList;
    public List<MonsterCardSO> Golens => _golemList;
    public List<MonsterCardSO> Witches => _witchList;
    public List<MonsterCardSO> Alchemists => _alchemistList;
    public List<MonsterCardSO> Beasts => _beastList;
    public List<MonsterCardSO> Demons => _demonList;

    public List<ArcaneCardSO> Arcanes => _arcaneList;
    public List<ArcaneCardSO> Traps => _trapList;

    [Header("Monsters")]
    [SerializeField] private List<MonsterCardSO> _angelList;
    [SerializeField] private List<MonsterCardSO> _dragonList;
    [SerializeField] private List<MonsterCardSO> _machineList;
    [SerializeField] private List<MonsterCardSO> _golemList;
    [SerializeField] private List<MonsterCardSO> _witchList;
    [SerializeField] private List<MonsterCardSO> _alchemistList;
    [SerializeField] private List<MonsterCardSO> _beastList;
    [SerializeField] private List<MonsterCardSO> _demonList;

    [Header("Arcanes")]
    [SerializeField] private List<ArcaneCardSO> _arcaneList;
    [SerializeField] private List<ArcaneCardSO> _trapList;

    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one CardsDatabase instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;
    }
}
