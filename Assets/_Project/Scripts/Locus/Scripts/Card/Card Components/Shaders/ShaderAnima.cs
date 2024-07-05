using UnityEngine;

public class ShaderAnima : MonoBehaviour{
    public Renderer _renderer;
    public CardVisual _controller;

    public void SetController(Renderer renderer, CardVisual controller){
        _renderer = renderer;
        _controller = controller;
    }

    public void SetAnimaColors(){
        var sideMat = new Material(_renderer.sharedMaterials[0]);
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        // var card = _controller.Card as MonsterCard;
        Color _anima1Color = SetAnimaColor((_controller.Card as MonsterCard).FirstAnima);
        Color _anima2Color = SetAnimaColor((_controller.Card as MonsterCard).SecondAnima);
        
        faceMat.SetColor("_Anima1Color", _anima1Color);
        faceMat.SetColor("_Anima2Color", _anima2Color);

        _controller.SetChangesToMaterial(sideMat, faceMat);
    }

    private Color SetAnimaColor(EAnimaType animaType){
        Color _newColor = new();

        switch (animaType){
            case EAnimaType.Mars:
                _newColor = GameManager.Instance.Visual.Color.Mars;
            break;
            case EAnimaType.Venus:
                _newColor = GameManager.Instance.Visual.Color.Venus;
            break;
            case EAnimaType.Jupiter:
                _newColor = GameManager.Instance.Visual.Color.Jupiter;
            break;
            case EAnimaType.Saturn:
                _newColor = GameManager.Instance.Visual.Color.Saturn;
            break;
            case EAnimaType.Mercury:
                _newColor = GameManager.Instance.Visual.Color.Mercury;
            break;
            case EAnimaType.Sun:
                _newColor = GameManager.Instance.Visual.Color.Sun;
            break;
            case EAnimaType.Moon:
                _newColor = GameManager.Instance.Visual.Color.Moon;
            break;
        }

        return _newColor;
    }
}