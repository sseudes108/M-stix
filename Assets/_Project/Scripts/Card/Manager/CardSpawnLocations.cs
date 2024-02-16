using UnityEngine;

public class CardSpawnLocations : MonoBehaviour{
    [SerializeField] private Transform _playerDeck, _enemyDeck;
    [SerializeField] private Transform _fusionCardSpawner;

    public Vector3 GetEnemyDeckPosition() => _enemyDeck.position;
    public Quaternion GetEnemyDeckRotation() => _enemyDeck.rotation;
    public Vector3 GetPlayerDeckPosition() => _playerDeck.position;
    public Quaternion GetPlayerDeckRotation() => _playerDeck.rotation;
    public Vector3 GetfusionCardSpawnerPosition() => _fusionCardSpawner.position;
    public Quaternion GetfusionCardSpawnerRotation() => _fusionCardSpawner.rotation;
}
