using UnityEngine;

[RequireComponent(typeof(Card))]
public class CardVisual : MonoBehaviour {
    private MeshRenderer _renderer;
    public CardShader Shader { get; private set; }

    public void Awake() {
        _renderer = GetComponentInChildren<MeshRenderer>();

        Shader = GetComponentInChildren<CardShader>();
        Shader.SetRenderer(_renderer);
    }

    public void SetVisuals(Texture2D illustration){
        Shader.SetCardImage(illustration);
        Shader.SetAnimaColors();
    }
}