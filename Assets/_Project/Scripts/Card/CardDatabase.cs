using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour {
    [SerializeField] private List<CardMonsterSO> _angel;
    [SerializeField] private List<CardMonsterSO> _dragon;
    [SerializeField] private List<CardMonsterSO> _golem;
    [SerializeField] private List<CardMonsterSO> _machina;

    //
    [SerializeField] private List<CardMonsterSO> _she;
    //

    public List<CardMonsterSO> Angels => _angel;
    public List<CardMonsterSO> Dragons => _dragon;
    public List<CardMonsterSO> Golens => _golem;
    public List<CardMonsterSO> Machinas => _machina;

    //
    public List<CardMonsterSO> She => _she;
    //
}