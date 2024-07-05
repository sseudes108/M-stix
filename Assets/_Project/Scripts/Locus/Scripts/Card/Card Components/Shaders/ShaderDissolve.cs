using System.Collections;
using UnityEngine;

public class ShaderDissolve : MonoBehaviour {
    private Renderer _renderer;
    private CardVisual _controller;
    private Card _card;
    public float CutOff {get; private set;} = 1f;
    private float _duration = 1f;
    private float _intensityFactor = 5f;

    public void SetController(Renderer renderer, CardVisual controller, Card card){
        _renderer = renderer;
        _controller = controller;
        _card = card;
    }

    public void MakeCardInvisible(){
        CutOff = 0;
    }

    public void DissolveCard(Color newColor){
        // Debug.Log("DissolveCard Called");
        StartCoroutine(DissolveRoutine(newColor));
    }

    private IEnumerator DissolveRoutine(Color newColor){
        var sideMat = new Material(_renderer.sharedMaterials[0]);
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        float elapsedTime = 0;
        Color adjustedColor = new(
            newColor.r * _intensityFactor, 
            newColor.g * _intensityFactor, 
            newColor.b * _intensityFactor,
            newColor.a
        );

        do{
            elapsedTime += Time.deltaTime;
            float interpolation = Mathf.Clamp01(elapsedTime / _duration);
            CutOff = Mathf.Lerp(1, 0, interpolation);

            faceMat.SetFloat("_CutOff", CutOff);
            faceMat.SetColor("_EdgeColor", adjustedColor);

            _controller.SetChangesToMaterial(sideMat, faceMat);

            if(CutOff > 0.5f){
                _card.EnableStatCanvas();
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }else{
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }

            yield return null;
        }while(CutOff > 0);
    }

    public void SolidifyCard(Color newColor){
        // Debug.Log("SolidifyCard Called");
        StartCoroutine(SolidifyRoutine(newColor));
    }

    private IEnumerator SolidifyRoutine(Color newColor){
        var sideMat = new Material(_renderer.sharedMaterials[0]);
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        float elapsedTime = 0;
        Color adjustedColor = new(
            newColor.r * _intensityFactor, 
            newColor.g * _intensityFactor, 
            newColor.b * _intensityFactor,
            newColor.a
        );

        do{
            elapsedTime += Time.deltaTime;
            float interpolation = Mathf.Clamp01(elapsedTime / _duration);
            CutOff = Mathf.Lerp(0, 1, interpolation);

            faceMat.SetFloat("_CutOff", CutOff);
            faceMat.SetColor("_EdgeColor", adjustedColor);

            _controller.SetChangesToMaterial(sideMat, faceMat);

            if(CutOff > 0.5f){
                _card.EnableStatCanvas();
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }else{
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }

            yield return null;
        }while(CutOff < 1);
    }
    
}