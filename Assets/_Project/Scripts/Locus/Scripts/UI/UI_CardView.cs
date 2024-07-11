using UnityEngine;

public class UI_CardView : MonoBehaviour{
    private CardMovement _movement;
    private Vector3 StartPosition;

    [SerializeField] private Renderer _renderer;

    private void Awake() {
        _movement = GetComponent<CardMovement>();
    }

    private void Start() {
        StartPosition = transform.position;
    }
}