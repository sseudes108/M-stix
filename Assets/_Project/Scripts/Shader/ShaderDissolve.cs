using UnityEngine;

public class ShaderDissolve : MonoBehaviour {
    [SerializeField] private float _dissolveSpeed;
    private float _cutOff = 1;
    private bool _dissolve = false;
    private bool _solidify = false;

    CardShaderController _shader;

    private void Awake() {
        _shader = GetComponentInParent<CardShaderController>();
    }

    private void Update() {
        if(_dissolve){
            DissolveCardEffect();
        }
        if(_solidify){
            SolidifyCardEffect();
        }
    }

    public void DissolveCard(){
        _dissolve = true;
    }
    public void SolidifyCard(){
        _solidify = true;
    }

    private void DissolveCardEffect(){
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        _cutOff = Mathf.MoveTowards(_cutOff, 0f, _dissolveSpeed * Time.deltaTime);

        faceMat.SetFloat("_CutOff", _cutOff);
        _shader.SetChangesToMaterial(faceMat);

        if(_cutOff < 0.5f){
            _shader.Card.DisableStatCanvas();
            _shader.Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }else{
            _shader.Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        if(_cutOff == 0f) {_dissolve = false;}
    }

    private void SolidifyCardEffect(){
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        _cutOff = Mathf.MoveTowards(_cutOff, 1f, _dissolveSpeed * Time.deltaTime);

        faceMat.SetFloat("_CutOff", _cutOff);
        _shader.SetChangesToMaterial(faceMat);

        if(_cutOff > 0.5f){
            _shader.Card.EnableStatCanvas();
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