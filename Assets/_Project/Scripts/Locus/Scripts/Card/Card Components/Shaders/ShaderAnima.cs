using UnityEngine;

public class ShaderAnima : MonoBehaviour{
    public Renderer _renderer;
    public CardVisual _controller;

    public void SetController(Renderer renderer, CardVisual controller){
        _renderer = renderer;
        _controller = controller;
    }

    public void SetAnimaColors(){
        var sideMat = new Material(_renderer.sharedMaterials[0]);
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        Color _anima1Color = Color.yellow;
        Color _anima2Color = Color.cyan;

        faceMat.SetColor("_Anima1Color", _anima1Color);
        faceMat.SetColor("_Anima2Color", _anima2Color);
        _controller.SetChangesToMaterial(sideMat, faceMat);
    }

    
}