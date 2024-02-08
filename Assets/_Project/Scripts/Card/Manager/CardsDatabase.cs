using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class CardsDatabase:MonoBehaviour{

        [SerializeField] private List<MonsterCardSO> _angels;
        [SerializeField] private List<MonsterCardSO> _dragons;
        [SerializeField] private List<MonsterCardSO> _machines;

        public List<MonsterCardSO> GetAngels() => _angels;
        public List<MonsterCardSO> GetDragons() => _dragons;
        public List<MonsterCardSO> GetMachines() => _machines;
    }
}