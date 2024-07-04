using UnityEngine;

public class ShaderDissolve :MonoBehaviour{
    private CardShader _shader;

    private float _dissolveSpeed = 10f;
    private float _cutOff = 1;
    private bool _dissolve = false;
    private bool _solidify = false;
    private Color _color;
    
    private void Update() {
        if(_dissolve){
            DissolveCardEffect(_color);
        }
        if(_solidify){
            SolidifyCardEffect(_color);
        }
    }
    
    public void SetController(CardShader shader) { _shader = shader; }

    public void DissolveCard(Color newColor){
        _color = newColor;
        _dissolve = true;
    }
    
    public void SolidifyCard(Color newColor){
        Debug.Log("SolidifyCard Called");
        _color = newColor;
        _solidify = true;
    }

    private void DissolveCardEffect(Color newColor){
        var sideMat = new Material(_shader.Renderer.sharedMaterials[0]);
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        _cutOff = Mathf.MoveTowards(_cutOff, 0f, _dissolveSpeed * Time.deltaTime);

        //Adjust to controle the brightness of the color (HDR)
        float intensityFactor = 2f;
        Color adjustedColor = new(
            newColor.r * intensityFactor, 
            newColor.g * intensityFactor, 
            newColor.b * intensityFactor,
            newColor.a
        );

        faceMat.SetFloat("_CutOff", _cutOff);
        faceMat.SetColor("_EdgeColor", adjustedColor);

        //Side
        sideMat.SetFloat("_CutOff", _cutOff);

        _shader.SetChangesToMaterial(sideMat, faceMat);

        if(_cutOff < 0.5f){
            // _shader.Card.DisableStatCanvas();
            _shader.Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }else{
            _shader.Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        if(_cutOff == 0f) {_dissolve = false;}
    }

    private void SolidifyCardEffect(Color newColor){
        var sideMat = new Material(_shader.Renderer.sharedMaterials[0]);
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        _cutOff = Mathf.MoveTowards(_cutOff, 1f, _dissolveSpeed / 2 * Time.deltaTime);

        //Adjust to controle the brightness of the color (HDR)
        float intensityFactor = 2f;
        Color adjustedColor = new(
            newColor.r * intensityFactor, 
            newColor.g * intensityFactor, 
            newColor.b * intensityFactor,
            newColor.a
        );

        faceMat.SetFloat("_CutOff", _cutOff);
        faceMat.SetColor("_EdgeColor", adjustedColor);

        // //Side
        sideMat.SetFloat("_CutOff", _cutOff);

        _shader.SetChangesToMaterial(sideMat, faceMat);

        if(_cutOff > 0.5f){
            // _shader.Card.EnableStatCanvas();
            _shader.Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }else{
            _shader.Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        if(_cutOff == 1f) {_solidify = false;}
    }

    public void MakeCardInvisible(){
        _cutOff = 0;
    }
}