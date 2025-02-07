namespace Mistix{
    using System.Collections.Generic;
    using UnityEngine;
    
    public class BoardManager : MonoBehaviour {
        [SerializeField] private BoardPlaceVisualController BoardPlaceVisualController;

        public List<BoardPlace> PlayerMonsterPlaces;
        public List<BoardPlace> PlayerArcanePlaces;
        public List<BoardPlace> EnemyMonsterPlaces;
        public List<BoardPlace> EnemyArcanePlaces;

        private void Awake() {
            BoardPlaceVisualController = new();
        }

        public void StartPhase(){
            BoardPlaceVisualController.LightUpPlaces(PlayerMonsterPlaces);
            BoardPlaceVisualController.LightUpPlaces(PlayerArcanePlaces);

            BoardPlaceVisualController.LightUpPlaces(EnemyMonsterPlaces);
            BoardPlaceVisualController.LightUpPlaces(EnemyArcanePlaces);
        }

    }
}