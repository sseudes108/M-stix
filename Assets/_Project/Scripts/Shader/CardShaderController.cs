using NUnit.Framework;
using UnityEngine;

public class CardShaderController : MonoBehaviour {
   
    private Renderer _renderer;
    private Card _card;

    //Dissolve
    private ShaderDissolve _dissolveShader;

    //Border
    private ShaderBorders _bordersShader;

    private void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
        _card = GetComponent<Card>();

        _dissolveShader = GetComponent<ShaderDissolve>();
        _bordersShader = GetComponent<ShaderBorders>();
    }

    private void Start() {
        SetCardImage();
        ResetBoarderColor();
    }

    public void SetChangesToMaterial(Material faceMat){
        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
    }

    private void SetCardImage(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", _card.Ilustration);

        SetChangesToMaterial(faceMat);
    }
    
    public void SetBoarderColor(Color newColor){
        _bordersShader.SetBoarderColor(newColor);
    }
    public void ResetBoarderColor(){
        _bordersShader.ResetBoarderColor();
    }

    public void DissolveCard(){
        _dissolveShader.DissolveCard();
    }
    public void SolidifyCard(){
        _dissolveShader.SolidifyCard();
    }

    public Renderer Renderer => _renderer;
    public Card Card => _card;
}