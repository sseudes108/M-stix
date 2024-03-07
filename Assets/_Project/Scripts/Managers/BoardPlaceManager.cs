using UnityEngine;

public class BoardPlaceManager : MonoBehaviour {
    [SerializeField] private BoardPlaceVisuals _boardPlaceVisuals;
    [SerializeField] private PlayerBoardPlaces _playerBoardPlaces;
    [SerializeField] private EnemyBoardPlaces _enemyBoardPlaces;

    private void Awake() {
        _boardPlaceVisuals = GetComponent<BoardPlaceVisuals>();
        _playerBoardPlaces = GetComponentInChildren<PlayerBoardPlaces>();
        _enemyBoardPlaces = GetComponentInChildren<EnemyBoardPlaces>();
    }

    public BoardPlaceVisuals BoardPlaceVisuals => _boardPlaceVisuals;
    public PlayerBoardPlaces PlayerBoardPlaces => _playerBoardPlaces;
    public EnemyBoardPlaces EnemyBoardPlaces => _enemyBoardPlaces;

    //Arcane Cards
    public Quaternion FaceDownRotation => Quaternion.Euler(-90, -180, 0);
    public Quaternion FaceUpRotation => Quaternion.Euler(90, 0, 0);

    //Monster Cards
    public Quaternion AttackFaceUpRotation => Quaternion.Euler(90, 0, 0);
    public Quaternion AttackFaceDownRotation => Quaternion.Euler(-90, -180, 0);

    public Quaternion DefenseFaceUpRotation => Quaternion.Euler(90, 0, -90);
    public Quaternion DefenseFaceDownRotation => Quaternion.Euler(-90, -180, -90);
}