using UnityEngine;

public class ShaderBorder : MonoBehaviour {
    private CardShader _shader;
    public void SetController(CardShader shader) { _shader = shader; }

    public void SetBorderColor(Color newColor){
        var sideMat = new Material(_shader.Renderer.sharedMaterials[0]);
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

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
        
        _shader.SetChangesToMaterial(sideMat, faceMat);
    }

    public void ResetBorderColor(){
        var sideMat = new Material(_shader.Renderer.sharedMaterials[0]);
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        faceMat.SetColor("_SelectedBorderColor", Color.black);
        faceMat.SetFloat("_Intensity", 0);

        _shader.SetChangesToMaterial(sideMat, faceMat);
    }
}