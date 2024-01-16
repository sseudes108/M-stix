using UnityEngine;

public class Player : MonoBehaviour{
    private InputManager _playerInputManager;
    private PlayerHand _playerHand;
    [SerializeField] private Vector2 _directionInputs;

    private void Awake() {
        _playerInputManager = GetComponent<InputManager>();
        _playerHand = GetComponent<PlayerHand>();
    }

    private void Update() {
        DebugUpdates();
    }

    private void DebugUpdates(){
        _directionInputs = _playerInputManager.FrameInput.Direction;
    }
}
