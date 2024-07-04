using UnityEngine;

[RequireComponent(typeof(Card))]
public class CardVisual : MonoBehaviour {
    public MeshRenderer Renderer  { get; private set; }
    public CardShader Shader { get; private set; }

    public void Awake() {
        Renderer = GetComponentInChildren<MeshRenderer>();
        Shader = GetComponentInChildren<CardShader>();
        Shader.SetRenderer(Renderer);
    }

    public void SetVisuals(Texture2D illustration){
        Shader.SetCardImage(illustration);
        Shader.SetAnimaColors();
    }
}