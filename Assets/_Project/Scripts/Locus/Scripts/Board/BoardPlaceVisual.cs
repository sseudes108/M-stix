using System.Collections;
using UnityEngine;

public class BoardPlaceVisual : MonoBehaviour {
    public BoardPlace _place;
    public Renderer[] _renderers;
    public Color LightUpColor;
    public Color DefaultColor;
    public float IntensityFactor;
    public bool IsFree => _place.IsFree;

    private void Awake() {
        _renderers = GetComponentsInChildren<Renderer>();
        _place = GetComponent<BoardPlace>();

        TurnOffLights();
    }

    public void TurnOffLights(){
        if(_place.IsPlayerPlace){
            StartCoroutine(SetColorRoutine(new Color(9f, 9f, 181f), 0.01f, true));
        }else{
            StartCoroutine(SetColorRoutine(new Color(181f, 9f, 9f), 0.01f, true));
        }
    }

    public void LightUp(){
        if(_place.IsPlayerPlace){
            StartCoroutine(SetColorRoutine(new Color(9f, 9f, 181f), 0.2f, false));
        }else{
            StartCoroutine(SetColorRoutine(new Color(181f, 9f, 9f), 0.2f, false));
        }
    }

    public void HighLight(){
        StartCoroutine(SetColorRoutine(new Color(216, 216, 27), 0.1f, false));
    }

    public IEnumerator SetColorRoutine(Color newColor, float intensity, bool imediate){
        Color adjustedColor = new(
            newColor.r * intensity, 
            newColor.g * intensity, 
            newColor.b * intensity,
            newColor.a
        );
        
        if(imediate){
            foreach(var renderer in _renderers){
                var newBorderMaterial = new Material(renderer.sharedMaterials[1]);
                ChangeMat(renderer, newBorderMaterial, adjustedColor, intensity);
            }
        }else{
            IntensityFactor = 0;
            var intensityTarget = intensity*2.5f;
            foreach(var renderer in _renderers){
                StartCoroutine(ChangeBoardColorRoutine(renderer, adjustedColor, IntensityFactor, intensityTarget));
            }
            yield return null;
        }
    }

    private void ChangeMat(Renderer renderer, Material newBorderMaterial, Color adjustedColor, float intensity){
        newBorderMaterial.SetColor("_BorderColor", adjustedColor);
        newBorderMaterial.SetFloat("_Intensity", intensity);
        renderer.materials = new[] { renderer.sharedMaterials[0], newBorderMaterial, renderer.sharedMaterials[2] };
    }

    private IEnumerator ChangeBoardColorRoutine(Renderer renderer, Color adjustedColor, float intensityFactor, float intensityTarget){
        var newBorderMaterial = new Material(renderer.sharedMaterials[1]);
        do{
            ChangeMat(renderer, newBorderMaterial, adjustedColor, intensityFactor);
            intensityFactor += Time.deltaTime;
            yield return null;
        }while(intensityFactor < intensityTarget);
        yield return null;
    }

    public void SetIntensityAnColor(Color lightUpColor, float intensityFactor){
        LightUpColor = lightUpColor;
        IntensityFactor = intensityFactor;
    }

    public void SetDefaultColor(Color defaultColor){
        DefaultColor = defaultColor;
    }
}