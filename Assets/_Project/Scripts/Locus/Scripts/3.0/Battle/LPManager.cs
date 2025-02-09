using System.Collections;
using UnityEngine;

namespace Mistix{    
    public class LPManager : MonoBehaviour {
        [SerializeField] private BattleManager _battleManager;

        private const int INITIALP = 8100;
        private int _playerLP;
        private int _enemyLP;

        public void ResetLifePoints(){
            _playerLP = 0;
            _enemyLP = 0;

            StartCoroutine(ResetPlayerLife());
            StartCoroutine(ResetEnemyLife());
        }

        private IEnumerator ResetPlayerLife(){
            while(_playerLP < 8100){
                _playerLP += 100;
                yield return new WaitForSeconds(0.01f);
                _battleManager.UpdateLifePoints(true, _playerLP);
            }

            if(_playerLP > INITIALP){
                _playerLP = INITIALP;
            }

            yield return null;
        }

        private IEnumerator ResetEnemyLife(){
            while(_enemyLP < 8100){
                _enemyLP += 100;
                yield return new WaitForSeconds(0.01f);
                _battleManager.UpdateLifePoints(false, _enemyLP);
            }

            if(_enemyLP > INITIALP){
                _enemyLP = INITIALP;
            }

            yield return null;
        }
    }
}