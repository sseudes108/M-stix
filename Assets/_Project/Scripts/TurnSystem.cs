using UnityEngine;

namespace Mistix{
    public class TurnSystem : MonoBehaviour{
        private static int _turn = 0;
        [SerializeField] private Transform _playerDeck, _enemyDeck;
        [SerializeField] private Transform _fusionCardSpawner;

        public void ChangeTurn(){
            _turn++;
        }
        public bool IsPlayerTurn(){
            return _turn % 2 == 0;
        }

        public int GetTurnNumber(){
            return _turn + 1;
        }

        public Vector3 GetEnemyDeckPosition() => _enemyDeck.position;
        public Quaternion GetEnemyDeckRotation() => _enemyDeck.rotation;
        public Vector3 GetPlayerDeckPosition() => _playerDeck.position;
        public Quaternion GetPlayerDeckRotation() => _playerDeck.rotation;
        public Vector3 GetfusionCardSpawnerPosition() => _fusionCardSpawner.position;
        public Quaternion GetfusionCardSpawnerRotation() => _fusionCardSpawner.rotation;
    }
}