using NUnit.Framework;
using UnityEngine;

public class CardShaderController : MonoBehaviour {
   
    private Renderer _renderer;
    private Card _card;

    //Dissolve
    private ShaderDissolve _dissolveShader;

    //Anima
    private AnimaShader _animaShader;

    //Border
    private ShaderBorders _bordersShader;

    private void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
        _card = GetComponent<Card>();

        _dissolveShader = GetComponent<ShaderDissolve>();
        _animaShader = GetComponent<AnimaShader>();
        _bordersShader = GetComponent<ShaderBorders>();
    }

    private void Start() {
        SetCardImage();
        ResetBoarderColor();
    }

    public void SetChangesToMaterial(Material sideMat, Material faceMat){
        _renderer.materials = new[] { sideMat, faceMat, _renderer.sharedMaterials[2] };
    }

    private void SetCardImage(){
        var sideMat = new Material(_renderer.sharedMaterials[0]);
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", _card.Ilustration);

        SetChangesToMaterial(sideMat,faceMat);
    }
    
    public void SetBoarderColor(Color newColor){
        _bordersShader.SetBoarderColor(newColor);
    }

    public void ResetBoarderColor(){
        _bordersShader.ResetBoarderColor();
    }

    public void DissolveCard(Color newColor){
        _dissolveShader.DissolveCard(newColor);
    }
    public void SolidifyCard(Color newColor){
        _dissolveShader.SolidifyCard(newColor);
    }
    public void MakeCardCardInvisible(){
        _dissolveShader.MakeCardInvisible();
    }

    public void SetSelectedAnimaShader(int animaIndex, EAnimaType selectedAnima){
        _animaShader.SetAnimaShader(animaIndex, selectedAnima);
    }

    public Renderer Renderer => _renderer;
    public Card Card => _card;
}