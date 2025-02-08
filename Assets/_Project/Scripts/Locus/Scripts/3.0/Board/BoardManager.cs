using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    
    public class BoardManager : MonoBehaviour {
        private BoardPlaceVisualController _boardPlaceVisualController;

        private Color PlayerDefaultColor;
        private Color EnemyDefaultColor;

        [SerializeField] private List<BoardPlace> _playerMonsterPlaces;
        [SerializeField] private List<BoardPlace> _playerArcanePlaces;
        [SerializeField] private List<BoardPlace> _enemyMonsterPlaces;
        [SerializeField] private List<BoardPlace> _enemyArcanePlaces;

        private void Awake() { _boardPlaceVisualController = new(); }

        public void LightUpAllPlaces(){
            _boardPlaceVisualController.LightUpPlaces(_playerMonsterPlaces, PlayerDefaultColor);
            _boardPlaceVisualController.LightUpPlaces(_playerArcanePlaces, PlayerDefaultColor);

            _boardPlaceVisualController.LightUpPlaces(_enemyMonsterPlaces, EnemyDefaultColor);
            _boardPlaceVisualController.LightUpPlaces(_enemyArcanePlaces, EnemyDefaultColor);
        }

        public void LightOffAllPlaces() { 
            _boardPlaceVisualController.LightOffPlaces(_playerMonsterPlaces, PlayerDefaultColor);
            _boardPlaceVisualController.LightOffPlaces(_playerArcanePlaces, PlayerDefaultColor);

            _boardPlaceVisualController.LightOffPlaces(_enemyMonsterPlaces, EnemyDefaultColor);
            _boardPlaceVisualController.LightOffPlaces(_enemyArcanePlaces, EnemyDefaultColor);
        }

        public void SetPlaceColors(Vector3 playerBoardColor, Vector3 enemyBoardColor){
            PlayerDefaultColor = new Color(playerBoardColor.x, playerBoardColor.y, playerBoardColor.z);
            EnemyDefaultColor = new Color(enemyBoardColor.x, enemyBoardColor.y, enemyBoardColor.z);
        }
    }
}