using UnityEngine;

public class ShaderAnima : MonoBehaviour{
    private CardShader _shader;
    public void SetController(CardShader shader) { _shader = shader; }

    public void SetAnimaColors(){
        var sideMat = new Material(_shader.Renderer.sharedMaterials[0]);
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        Color _anima1Color = Color.yellow;
        Color _anima2Color = Color.cyan;

        faceMat.SetColor("_Anima1Color", _anima1Color);
        faceMat.SetColor("_Anima2Color", _anima2Color);
        _shader.SetChangesToMaterial(sideMat, faceMat);
    }
    
    // public void SetSelectedAnima(int animaIndex, EAnimaType selectedAnima){
    //     var sideMat = new Material(_shader.Renderer.sharedMaterials[0]);
    //     var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

    //     Color _anima1Color = new();
    //     Color _anima2Color = new();

    //     if(animaIndex == 1){
    //         // _anima1Color = BattleManager.Instance.ColorManager.GetAnimaColor(selectedAnima);
    //         // _anima2Color = BattleManager.Instance.ColorManager.BlackColor;
    //     }else if(animaIndex == 2){
    //         // _anima1Color = BattleManager.Instance.ColorManager.BlackColor;
    //         // _anima2Color = BattleManager.Instance.ColorManager.GetAnimaColor(selectedAnima);
    //     }

    //     faceMat.SetColor("_Anima1Color", _anima1Color);
    //     faceMat.SetColor("_Anima2Color", _anima2Color);
    //     _shader.SetChangesToMaterial(sideMat, faceMat);
    // }
}