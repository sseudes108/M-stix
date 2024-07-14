using UnityEngine;

public class CardVisual : MonoBehaviour {
    [SerializeField] protected ColorManagerSO _colorManager;
    public Renderer Renderer { get; private set; }
    public ShaderAnima Anima { get; private set; }
    public ShaderBorder Border { get; private set; }
    public ShaderDissolve Dissolve { get; private set; }
    public Card Card { get; private set; }

    private void Awake() {
        SetComponents();
    }

    public void SetVisuals(Texture2D illustration){
        var sideMat = new Material(Renderer.sharedMaterials[0]);
        var faceMat = new Material(Renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", illustration);

        SetChangesToMaterial(sideMat,faceMat);
        if(Card is MonsterCard){
            Anima.AnimaNotSelectedColors();
        }
    }

    public void SetChangesToMaterial(Material sideMat, Material faceMat){
        Renderer.materials = new[] { sideMat, faceMat, Renderer.sharedMaterials[2] };
    }

    public void DisableRenderer(){
        Renderer.enabled = false;
    }

    public void EnableRenderer(){
        Renderer.enabled = true;
    }

    public void SetComponents(){
        Card = GetComponent<Card>();
        Renderer = GetComponentInChildren<Renderer>();

        Anima = GetComponentInChildren<ShaderAnima>();
        Anima.SetController(Renderer, this, _colorManager);

        Border = GetComponentInChildren<ShaderBorder>();
        Border.SetController(Renderer, this);

        Dissolve = GetComponentInChildren<ShaderDissolve>();
        Dissolve.SetController(Renderer, this, Card);
    }

    public float GetCutoff(){
        return Dissolve.CutOff;
    }
}