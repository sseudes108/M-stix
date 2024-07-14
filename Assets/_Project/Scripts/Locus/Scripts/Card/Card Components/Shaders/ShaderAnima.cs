using UnityEngine;

public class ShaderAnima : MonoBehaviour{
    private ColorSO _colorManager;
    public Renderer _renderer;
    public CardVisual _controller;

    public void SetController(Renderer renderer, CardVisual controller, ColorSO colorManager){
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
                // newColor = GameManager.Instance.Visual.Color.Mars;
            break;
            case EAnimaType.Venus:
                newColor = _colorManager.Venus;
                // newColor = GameManager.Instance.Visual.Color.Venus;
            break;
            case EAnimaType.Jupiter:
                newColor = _colorManager.Jupiter;
                // newColor = GameManager.Instance.Visual.Color.Jupiter;
            break;
            case EAnimaType.Saturn:
                newColor = _colorManager.Saturn;
                // newColor = GameManager.Instance.Visual.Color.Saturn;
            break;
            case EAnimaType.Mercury:
                newColor = _colorManager.Mercury;
                // newColor = GameManager.Instance.Visual.Color.Mercury;
            break;
            case EAnimaType.Sun:
                newColor = _colorManager.Sun;
                // newColor = GameManager.Instance.Visual.Color.Sun;
            break;
            case EAnimaType.Moon:
                newColor = _colorManager.Moon;
                // newColor = GameManager.Instance.Visual.Color.Moon;
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