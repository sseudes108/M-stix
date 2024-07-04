using UnityEngine;

public class UI_CardView : MonoBehaviour{
    private CardMovement _movement;
    public Transform _offCam;
    private Vector3 StartPosition;

    [SerializeField] private Renderer _renderer;
    private readonly float _moveSpeed = 3f;

    private void OnEnable() {
        // UIBattleScene.OnSelectionFinished += UIBattleScene_OnSelectionFinished;
        Card.OnMouseOverCard += Card_OnMouseOverCard;
    }

    private void OnDisable() {
        // UIBattleScene.OnSelectionFinished -= UIBattleScene_OnSelectionFinished;
        Card.OnMouseOverCard -= Card_OnMouseOverCard;
    }

    private void Card_OnMouseOverCard(Texture2D illustration){
        ChangeIllustration(illustration);
    }

    // private void UIBattleScene_OnSelectionFinished(){
    //     _movement.SetTargetPosition(_offCam.position, Quaternion.identity, _moveSpeed);
    // }

    private void Awake() {
        _movement = GetComponent<CardMovement>();
    }

    private void Start() {
        StartPosition = transform.position;
    }

    private void ChangeIllustration(Texture2D illustration){
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", illustration);
        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
    }
}