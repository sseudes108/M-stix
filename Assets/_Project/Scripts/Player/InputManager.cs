using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour{
    public FrameInput FrameInput => _frameInput;

    private Inputs _input;
    private InputAction _direction, _selected;
    private FrameInput _frameInput;

    private void OnEnable() {
        _input.Enable();
    }

    private void OnDisable() {
        _input.Disable();
    }

    private void Awake() {
        _input = new Inputs();
        _direction = _input.PlayerInputs.Direction;
        _selected = _input.PlayerInputs.Selection;
    }

    private void Update() {
        _frameInput = GatherInputs();
    }

    private FrameInput GatherInputs(){
        return new FrameInput{
            Direction = _direction.ReadValue<Vector2>(),
            Selection = _selected.WasPressedThisFrame()
        };
    }

}

public struct FrameInput{
   public Vector2 Direction;
   public bool Selection;
}
