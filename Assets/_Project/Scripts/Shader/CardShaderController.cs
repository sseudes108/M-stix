using UnityEngine;

public class CardShaderController : MonoBehaviour {
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected Card _card;

    private void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
        _card = GetComponent<Card>();
    }

    private void Start() {
        SetCardImage();
    }

    private void SetCardImage(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", _card.Ilustration);
        _renderer.materials = new[]{_renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2]};
    }
}