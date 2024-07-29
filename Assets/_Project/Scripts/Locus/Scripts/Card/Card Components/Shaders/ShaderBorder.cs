using UnityEngine;

public class ShaderBorder : MonoBehaviour {
    public Renderer _renderer;
    public CardVisual _controller;

    public void SetController(Renderer renderer, CardVisual controller){
        _renderer = renderer;
        _controller = controller;
    }

    public void SetBorderColor(Color newColor){
        var sideMat = new Material(_renderer.sharedMaterials[0]);
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        //Adjust to controle the brightness of the color (HDR)
        float intensityFactor = 0.02f;
        Color adjustedColor = new(
            newColor.r * intensityFactor, 
            newColor.g * intensityFactor, 
            newColor.b * intensityFactor,
            newColor.a
        );

        faceMat.SetColor("_SelectedBorderColor", adjustedColor);
        faceMat.SetFloat("_Intensity", 1f);
        
        _controller.SetChangesToMaterial(sideMat, faceMat);
    }

    public void ResetBorderColor(){
        if(_renderer == null) { return; }
        
        var sideMat = new Material(_renderer.sharedMaterials[0]);
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        faceMat.SetColor("_SelectedBorderColor", Color.black);
        faceMat.SetFloat("_Intensity", 0);

        _controller.SetChangesToMaterial(sideMat, faceMat);
    }
}