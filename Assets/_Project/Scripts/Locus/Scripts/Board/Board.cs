using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] private BoardManagerSO _boardManager;

    public List<BoardPlace> PlayerMonsterPlaces;
    public List<BoardPlace> PlayerArcanePlaces;
    public List<BoardPlace> EnemyMonsterPlaces;
    public List<BoardPlace> EnemyArcanePlaces;

    public BoardPlaceVisualController BoardPlaceVisualController;

    private void OnEnable() {
        BoardPlaceVisualController ??= new(PlayerMonsterPlaces, PlayerArcanePlaces, EnemyMonsterPlaces, EnemyArcanePlaces);
    }

    private void Start(){
        _boardManager.SetBoardPlaceVisualController(BoardPlaceVisualController);
    }
}