using UnityEngine;

public class UICardPlaceHolder : MonoBehaviour{
    private Renderer _renderer;

    private void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void ChangeIllustration(Texture2D newIlustration){
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        faceMat.SetTexture("_Ilustration", newIlustration);

        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
    }
}