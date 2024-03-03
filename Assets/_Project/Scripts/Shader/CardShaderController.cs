using NUnit.Framework;
using UnityEngine;

public class CardShaderController : MonoBehaviour {
    protected Renderer _renderer;
    protected Card _card;

    [Header("Dissolve")]
    [SerializeField] private float _dissolveSpeed;
    private float _cutOff = 1;
    private bool _dissolve = false;
    private bool _solidify = false;

    private void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
        _card = GetComponent<Card>();
    }

    private void Start() {
        SetCardImage();
        ResetBoarderColor();
    }

    private void Update() {
        if(_dissolve){
            DissolveCardEffect();
        }
        if(_solidify){
            SolidifyCardEffect();
        }
    }

    private void SetCardImage(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", _card.Ilustration);

        SetChangesToMaterial(faceMat);
    }

    private void SetChangesToMaterial(Material faceMat){
        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
    }

    public void SetBoarderColor(Color newColor){
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        //Adjust to controle the brightness of the color (HDR)
        float intensityFactor = 0.01f;
        Color adjustedColor = new Color(
            newColor.r * intensityFactor, 
            newColor.g * intensityFactor, 
            newColor.b * intensityFactor,
            newColor.a
        );

        faceMat.SetColor("_SelectedBorderColor", adjustedColor);
        faceMat.SetFloat("_Intensity", 1.5f);
        
        SetChangesToMaterial(faceMat);
    }

    public void ResetBoarderColor(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        faceMat.SetColor("_SelectedBorderColor", Color.black);
        faceMat.SetFloat("_Intensity", 0);

        SetChangesToMaterial(faceMat);
    }

    public void DissolveCard(){
        _dissolve = true;
    }

    private void DissolveCardEffect(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        _cutOff = Mathf.MoveTowards(_cutOff, 0f, _dissolveSpeed * Time.deltaTime);

        faceMat.SetFloat("_CutOff", _cutOff);
        SetChangesToMaterial(faceMat);

        if(_cutOff < 0.5f){
            _card.DisableStatCanvas();
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }else{
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        if(_cutOff == 0f) {_dissolve = false;}
    }

    private void SolidifyCardEffect(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        _cutOff = Mathf.MoveTowards(_cutOff, 1f, _dissolveSpeed * Time.deltaTime);

        faceMat.SetFloat("_CutOff", _cutOff);
        SetChangesToMaterial(faceMat);

        if(_cutOff > 0.5f){
            _card.EnableStatCanvas();
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }else{
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        if(_cutOff == 1f) {_solidify = false;}
    }
}