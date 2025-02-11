using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class MonsterFusion : MonoBehaviour {
        public List<MonsterCardSO> _strongestTypeList = new();
        [SerializeField] CardDatabaseSO _cardDatabase;
        private FusionManager _fusionManager;

        private void Awake() {
            _fusionManager = GetComponent<FusionManager>();
        }

        public void StartMonsterFusionRoutine(MonsterCard monster1, MonsterCard monster2){
            StartCoroutine(MonsterFusionRoutine(monster1, monster2));
        }

        private IEnumerator MonsterFusionRoutine(MonsterCard monster1, MonsterCard monster2){
            var monster1Lvl = monster1.Level;
            var monster2Lvl = monster2.Level;

            var monster1Atk = monster1.Attack;
            var monster2Atk = monster2.Attack;

            yield return null;

            //Fusion Failed
            if(monster1Lvl != monster2Lvl){
                //Not equal levels
                _fusionManager.FusionFailed(monster1, monster2);
                yield break;
            }

            //Fusion Sucess//
            //Get Strongest Monster Type
            EMonsterType strongestMonsterType;
            if(monster1Atk > monster2Atk){
                strongestMonsterType = monster1.MonsterType;
            }else{
                strongestMonsterType = monster2.MonsterType;
            }
            yield return null;

            //List of the possible monsters (Correct lvl)
            _strongestTypeList = _cardDatabase.GetStrongestTypeList(strongestMonsterType);
            yield return null;

            var possibleMonsters = SetPossibleMonstersList(monster1Lvl);

            //Instantiate fusioned card
            var randomIndex = Random.Range(0, possibleMonsters.Count);
            var fusionedCard = Instantiate(_fusionManager.CreateCard(possibleMonsters[randomIndex]));
            fusionedCard.name = $"ID {fusionedCard.GetInstanceID()} - Fusioned";
            fusionedCard.SetFusionedCard();

            // make card invisible
            fusionedCard.MakeCardInvisible();
            fusionedCard.DisableRenderer();
            fusionedCard.DisableStatCanvas();

            // Fusion
            _fusionManager.FusionSucess(monster1, monster2, fusionedCard);

            //Make Visibel
            yield return new WaitForSeconds(2f);
            fusionedCard.EnableRenderer();
            fusionedCard.SolidifyCard(Color.white);
            yield return null;
        }

        private List<MonsterCardSO> SetPossibleMonstersList(int monsterLvl){
            List<MonsterCardSO> possibleMonsters = new();

            foreach(var monster in _strongestTypeList){
                if(monster.Level == monsterLvl + 1){
                    possibleMonsters.Add(monster);
                }
            }

            return possibleMonsters;
        }
    }
}