using UnityEngine;

namespace Mistix{
    public class TurnSystem : MonoBehaviour{
        public static TurnSystem Instance;
        private static int _turn = 0;
        [SerializeField] private Transform _playerDeck, _enemyDeck;

        private void Awake() {
            if(Instance != null){
                Errors.InstanceError(this);
            }
            Instance = this;
        }

        public void ChangeTurn(){
            _turn++;
        }
        public static bool IsPlayerTurn(){
            return _turn % 2 == 0;
        }

        public static int GetTurnNumber(){
            return _turn + 1;
        }
        public Vector3 GetDeckPosition(){
            if(IsPlayerTurn()){
                return _playerDeck.position;
            }else{
                return _enemyDeck.position;
            }
        }

        public Quaternion GetDeckRotation(){
            if(IsPlayerTurn()){
                return _playerDeck.rotation;
            }else{
                return _enemyDeck.rotation;
            }
        }
    }
}