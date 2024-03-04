using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceVisuals : MonoBehaviour {
    private BoardCardPlacement _boardCardPlacement;

    private void Awake() {
        _boardCardPlacement = GetComponent<BoardCardPlacement>();
    }

    public void BoardPlacementGlowEffect(){

    }

    public void SetBoarderColor(Renderer[] _renderers, Color newColor){
        var borderMat = new Material(_renderers[0].sharedMaterials[1]);

        //Adjust to controle the brightness of the color (HDR)
        float intensityFactor = 0.1f;
        Color adjustedColor = new Color(
            newColor.r * intensityFactor, 
            newColor.g * intensityFactor, 
            newColor.b * intensityFactor,
            newColor.a
        );

        borderMat.SetColor("_SelectedBorderColor", adjustedColor);
        borderMat.SetFloat("_Intensity", 2f);
        borderMat.SetFloat("_TimeMultiplier", 2f);

        foreach(var renderer in _renderers){
            renderer.materials = new[] {renderer.sharedMaterials[0], borderMat, renderer.sharedMaterials[2] };
        }
    }

    public void ResetBoarderColor(Renderer[] _renderers){
        var materials = new List<Material>();
        var borderMat = new Material(_renderers[0].sharedMaterials[1]);

        foreach(var renderer in _renderers){
            materials.Add(renderer.sharedMaterials[1]);
        }

        Color color;
        if(_boardCardPlacement is BoardCardMonsterPlace){
            color = Color.blue;
        }else{
            color = Color.red;
        }

        borderMat.SetColor("_SelectedBorderColor", color);
        borderMat.SetFloat("_Intensity", 1.5f);
        borderMat.SetFloat("_TimeMultiplier", 0f);

        foreach(var renderer in _renderers){
            renderer.materials = new[] {renderer.sharedMaterials[0], borderMat, renderer.sharedMaterials[2] };
        }
    }






















    public void SetBoarderColor(Renderer _renderer, Color newColor){
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        //Adjust to controle the brightness of the color (HDR)
        float intensityFactor = 0.1f;
        Color adjustedColor = new Color(
            newColor.r * intensityFactor, 
            newColor.g * intensityFactor, 
            newColor.b * intensityFactor,
            newColor.a
        );

        faceMat.SetColor("_SelectedBorderColor", adjustedColor);
        faceMat.SetFloat("_Intensity", 2f);
        faceMat.SetFloat("_TimeMultiplier", 2f);

        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
    }

    public void ResetBoarderColor(Renderer _renderer){
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        Color color;
        if(_boardCardPlacement is BoardCardMonsterPlace){
            color = Color.blue;
        }else{
            color = Color.red;
        }

        faceMat.SetColor("_SelectedBorderColor", color);
        faceMat.SetFloat("_Intensity", 1.5f);
        faceMat.SetFloat("_TimeMultiplier", 0f);

        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
    }
}