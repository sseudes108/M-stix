using UnityEngine;

public class ShaderBorders : MonoBehaviour {
    CardShaderController _shader;

    private void Awake() {
        _shader = GetComponentInParent<CardShaderController>();
    }

    public void SetBoarderColor(Color newColor){
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        //Adjust to controle the brightness of the color (HDR)
        float intensityFactor = 0.02f;
        Color adjustedColor = new Color(
            newColor.r * intensityFactor, 
            newColor.g * intensityFactor, 
            newColor.b * intensityFactor,
            newColor.a
        );

        faceMat.SetColor("_SelectedBorderColor", adjustedColor);
        faceMat.SetFloat("_Intensity", 1f);
        
        _shader.SetChangesToMaterial(faceMat);
    }

    public void ResetBoarderColor(){
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        faceMat.SetColor("_SelectedBorderColor", Color.black);
        faceMat.SetFloat("_Intensity", 0);

        _shader.SetChangesToMaterial(faceMat);
    }
}