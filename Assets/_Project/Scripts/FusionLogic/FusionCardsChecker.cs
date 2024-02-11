using System.Collections.Generic;
using Mistix;
using Mistix.Enums;
using UnityEngine;

public class FusionCardsChecker : MonoBehaviour{
        public List<MonsterCardSO> GetPossibleMonsterList(MonsterCard monster1, MonsterCard monster2, int monster1Level){
            var monster1Atk = monster1.GetMonsterAtk();
            var monster2Atk = monster2.GetMonsterAtk();

            EMonsterType strongestMonsterType;
            if (monster2Atk < monster1Atk)
            {
                strongestMonsterType = monster1.GetMonsterType();
            }
            else
            {
                strongestMonsterType = monster2.GetMonsterType();
            }

            List<MonsterCardSO> strongestMonsterList = new();
            switch (strongestMonsterType)
            {
                case EMonsterType.Angel:
                    strongestMonsterList = BattleManager.Instance.CardsDatabase.GetAngels();
                    break;
                case EMonsterType.Machine:
                    strongestMonsterList = BattleManager.Instance.CardsDatabase.GetMachines();
                    break;
                case EMonsterType.Dragon:
                    strongestMonsterList = BattleManager.Instance.CardsDatabase.GetDragons();
                    break;
                default:
                    Debug.Log("Error getting the strongest monster list");
                    break;
            }

            List<MonsterCardSO> possibleMonsterList = new();
            foreach (var monster in strongestMonsterList)
            {
                if (monster.Level == monster1Level + 1)
                {
                    possibleMonsterList.Add(monster);
                }
            }

            return possibleMonsterList;
        }
}
