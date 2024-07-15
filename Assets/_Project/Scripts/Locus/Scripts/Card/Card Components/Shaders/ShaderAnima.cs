using UnityEngine;

public class ShaderAnima : MonoBehaviour{
    private ColorDatabaseSO _colorManager;
    public Renderer _renderer;
    public CardVisual _controller;

    public void SetController(Renderer renderer, CardVisual controller, ColorDatabaseSO colorManager){
        _renderer = renderer;
        _controller = controller;
        _colorManager = colorManager;
    }

    public void Anima1Selected(){
        Color anima1Color = SetAnimaColor(GetComponentInParent<MonsterCard>().FirstAnima);
        Color anima2Color = Color.black;
        SetAnimaColors(anima1Color, anima2Color);
    }
    
    public void Anima2Selected(){
        Color anima1Color = Color.black;
        Color anima2Color = SetAnimaColor(GetComponentInParent<MonsterCard>().SecondAnima);
        SetAnimaColors(anima1Color, anima2Color);
    }

    public void AnimaNotSelectedColors(){
        // Debug.Log("AnimaNotSelectedColors");
        Color anima1Color = SetAnimaColor(GetComponentInParent<MonsterCard>().FirstAnima);
        Color anima2Color = SetAnimaColor(GetComponentInParent<MonsterCard>().SecondAnima);
        SetAnimaColors(anima1Color, anima2Color);
    }

    public void SetAnimaColors(Color anima1Color, Color anima2Color){
        var sideMat = new Material(_renderer.sharedMaterials[0]);
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        faceMat.SetColor("_Anima1Color", anima1Color);
        faceMat.SetColor("_Anima2Color", anima2Color);

        _controller.SetChangesToMaterial(sideMat, faceMat);
    }

    private Color SetAnimaColor(EAnimaType animaType){
        Color newColor = new();
        float intensityFactor = 10f;

        switch (animaType){
            case EAnimaType.Mars:
                newColor = _colorManager.Mars;
            break;
            case EAnimaType.Venus:
                newColor = _colorManager.Venus;
            break;
            case EAnimaType.Jupiter:
                newColor = _colorManager.Jupiter;
            break;
            case EAnimaType.Saturn:
                newColor = _colorManager.Saturn;
            break;
            case EAnimaType.Mercury:
                newColor = _colorManager.Mercury;
            break;
            case EAnimaType.Sun:
                newColor = _colorManager.Sun;
            break;
            case EAnimaType.Moon:
                newColor = _colorManager.Moon;
            break;
        }

        Color adjustedColor = new(
            newColor.r * intensityFactor, 
            newColor.g * intensityFactor, 
            newColor.b * intensityFactor,
            newColor.a
        );

        return adjustedColor;
    }
}