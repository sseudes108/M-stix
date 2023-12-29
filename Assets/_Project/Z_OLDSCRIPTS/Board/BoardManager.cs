using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour{
    public static BoardManager Instance;
    public List<BoardPlacementController> PlayerMonsterPlaces => _playerMonsterPlaces;
    public List<BoardPlacementController> PlayerArcanePlaces => _playerArcanePlaces;
    public List<BoardPlacementController> EnemyMonsterPlaces => _enemyMonsterPlaces;
    public List<BoardPlacementController> EnemyArcanePlaces => _enemyArcanePlaces;

    [SerializeField] private List<BoardPlacementController> _playerMonsterPlaces;
    [SerializeField] private List<BoardPlacementController> _playerArcanePlaces;
    [SerializeField] private List<BoardPlacementController> _enemyMonsterPlaces;
    [SerializeField] private List<BoardPlacementController> _enemyArcanePlaces;

    private void Awake() {
        if(Instance == null){Instance = this;}
    }

    public void OcuppyPlace(List<BoardPlacementController> listOfPlaces, BoardPlacementController place){
        place.BoardPlaceOccupied = true;
    }
    
    public void VacatePlace(List<BoardPlacementController> listOfPlaces, BoardPlacementController place){
        place.BoardPlaceOccupied = false;
    }
}
