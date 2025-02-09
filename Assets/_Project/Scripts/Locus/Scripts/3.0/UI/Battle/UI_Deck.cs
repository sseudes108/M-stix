using System.Collections;
using UnityEngine;

namespace Mistix{    
    public class UI_Deck : MonoBehaviour {
        [SerializeField] private BattleManager _battleManager;

        private const int DECKCARDS = 42;
        private int _playerDeck;
        private int _enemyDeck;

        public void ResetDeckCount(){
            _playerDeck = 0;
            _enemyDeck = 0;

            StartCoroutine(ResetPlayerDeckCount());
            StartCoroutine(ResetEnemyDeckCount());
        }

        private IEnumerator ResetPlayerDeckCount(){
            while(_playerDeck < DECKCARDS){
                _playerDeck += 1;
                yield return new WaitForSeconds(0.02f);
                _battleManager.UpdateDeckCount(true, _playerDeck);
            }

            if(_playerDeck > DECKCARDS){
                _playerDeck = DECKCARDS;
            }

            yield return null;
        }

        private IEnumerator ResetEnemyDeckCount(){
            while(_enemyDeck < DECKCARDS){
                _enemyDeck += 1;
                yield return new WaitForSeconds(0.02f);
                _battleManager.UpdateDeckCount(false, _enemyDeck);
            }

            if(_enemyDeck > DECKCARDS){
                _enemyDeck = DECKCARDS;
            }

            yield return null;
        }
    }
}