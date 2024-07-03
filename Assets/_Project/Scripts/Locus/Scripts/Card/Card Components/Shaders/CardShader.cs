using UnityEngine;

public class CardShader : MonoBehaviour {
    public Renderer Renderer {get; private set;}
    public Card Card {get; private set;}
    private ShaderDissolve _dissolveShader;
    private ShaderAnima _animaShader;
    private ShaderBorder _borderShader;


    private void Awake(){
        _borderShader = GetComponent<ShaderBorder>();
        _borderShader.SetController(this);

        _animaShader = GetComponent<ShaderAnima>();
        _animaShader.SetController(this);
    }

    public void SetRenderer(Renderer renderer){
        Renderer = renderer;
    }

    public void SetChangesToMaterial(Material sideMat, Material faceMat){
        Renderer.materials = new[] { sideMat, faceMat, Renderer.sharedMaterials[2] };
    }

    public void SetCardImage(Texture2D illustration){
        var sideMat = new Material(Renderer.sharedMaterials[0]);
        var faceMat = new Material(Renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", illustration);

        SetChangesToMaterial(sideMat, faceMat);
    }

    public void SetBoarderColor(Color newColor){
        _borderShader.SetBorderColor(newColor);
    }

    public void ResetBoarderColor(){
        _borderShader.ResetBorderColor();
    }

    public void SetAnimaColors(){
        _animaShader.SetAnimaColors();
    }

    // private void Awake() {
    //     SetComponents();
    // }

    // // private void Start() {
    // //     SetCardImage();
    // //     ResetBoarderColor();
    // // }

    // private void SetComponents(){
    //     _dissolveShader = GetComponent<ShaderDissolve>();
    //     _dissolveShader.SetShaderController(this);
        
    //     _animaShader = GetComponent<ShaderAnima>();
    //     _animaShader.SetShaderController(this);

    //     _borderShader = GetComponent<ShaderBorder>();
    //     _borderShader.SetShaderController(this);
    // }

    // public void SetRenderer(Renderer renderer){
    //     Renderer = renderer;
    // }

    // public void SetCard(Card card){
    //     Card = card;
    // }

    // public void SetChangesToMaterial(Material sideMat, Material faceMat){
    //     Renderer.materials = new[] { sideMat, faceMat, Renderer.sharedMaterials[2] };
    // }

    // public void SetCardImage(Texture2D illustration){
    //     var faceMat = new Material(Renderer.sharedMaterials[1]);
    //     faceMat.SetTexture("_Ilustration", illustration);
    //     Renderer.materials = new[] { Renderer.sharedMaterials[0], faceMat, Renderer.sharedMaterials[2]};
    //     // var sideMat = new Material(Renderer.sharedMaterials[0]);
    //     // var faceMat = new Material(Renderer.sharedMaterials[1]);
    //     // // faceMat.SetTexture("_Ilustration", _card.Ilustration);

    //     // SetChangesToMaterial(sideMat,faceMat);
    // }
    
    // public void SetBoarderColor(Color newColor){
    //     _borderShader.SetBorderColor(newColor);
    // }

    // public void ResetBoarderColor(){
    //     _borderShader.ResetBorderColor();
    // }

    // public void DissolveCard(Color newColor){
    //     _dissolveShader.DissolveCard(newColor);
    // }

    // public void SolidifyCard(Color newColor){
    //     _dissolveShader.SolidifyCard(newColor);
    // }

    // public void MakeCardCardInvisible(){
    //     _dissolveShader.MakeCardInvisible();
    // }

    // public void SetSelectedAnimaShader(int animaIndex, EAnimaType selectedAnima){
    //     _animaShader.SetAnimaShader(animaIndex, selectedAnima);
    // }
}