using UnityEngine;

namespace Mistix{
    [CreateAssetMenu(fileName = "MonsterCardSO", menuName = "Mistix/Card/Monster", order = 0)]
    public class MonsterCardSO : CardSO {
        public EMonsterType MonsterType;
        public EAnimaType FirstAnima, SecondAnima;
        public string Description;
        
        [Range(1,8)]
        public int Level = 1;
        public int Attack;
        public int Deffense;
    }
}