using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceManager : MonoBehaviour {
    [SerializeField] private List<Transform> _playerMonsterPlaces;
    [SerializeField] private List<Transform> _playerArcanePlaces;
    [SerializeField] private List<Transform> _enemyMonsterPlaces;
    [SerializeField] private List<Transform> _enemyArcanePlaces;

    public List<Transform> PlayerMonsterPlaces => _playerMonsterPlaces;
    public List<Transform> PlayerArcanePlaces => _playerArcanePlaces;
    public List<Transform> EnemyMonsterPlaces => _enemyMonsterPlaces;
    public List<Transform> EnemyArcanePlaces => _enemyArcanePlaces;
}