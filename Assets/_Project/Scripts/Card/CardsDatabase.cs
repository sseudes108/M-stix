using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class CardsDatabase:MonoBehaviour{
        public static CardsDatabase Instance;

        [SerializeField] private List<MonsterCardSO> _angels;
        [SerializeField] private List<MonsterCardSO> _dragons;
        [SerializeField] private List<MonsterCardSO> _machines;

        private void Awake() {
            if(Instance != null){
                Errors.InstanceError(this);
            }
            Instance = this;
        }

        public List<MonsterCardSO> GetAngels() => _angels;
        public List<MonsterCardSO> GetDragons() => _dragons;
        public List<MonsterCardSO> GetMachines() => _machines;
    }
}