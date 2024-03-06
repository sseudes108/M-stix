using UnityEngine;

public class AnimaShader : MonoBehaviour {
    private CardShaderController _shader;
    Color _anima1Color;
    Color _anima2Color;
    Color _animaSelectedColor;
    Color _defaulColor = Color.black;

    private void Awake() {
        _shader = GetComponentInParent<CardShaderController>();
    }

    public void SetAnimaShader(int animaSelected){
        var faceMat = new Material(_shader.Renderer.sharedMaterials[1]);

        if(animaSelected == 0){
            Debug.Log("Anima 1");
            _anima1Color = new Color(216, 8, 54);
            _anima2Color = _defaulColor;

            // float intensityFactor = 2f;
            // Color adjustedColor = new(
            //     _animaSelectedColor.r * intensityFactor, 
            //     _animaSelectedColor.g * intensityFactor, 
            //     _animaSelectedColor.b * intensityFactor,
            //     _animaSelectedColor.a
            // );

            // _anima1Color = adjustedColor;

        }else if(animaSelected == 1){
            Debug.Log("Anima 2");
            _anima1Color = _defaulColor;
            _anima2Color = new Color(216, 8, 54);

            // float intensityFactor = 2f;
            // Color adjustedColor = new(
            //     _anima2Color.r * intensityFactor, 
            //     _anima2Color.g * intensityFactor, 
            //     _anima2Color.b * intensityFactor,
            //     _anima2Color.a
            // );

            // _anima2Color = adjustedColor;
        }

        faceMat.SetColor("_Anima1Color", _anima1Color);
        faceMat.SetColor("_Anima1Color", _anima2Color);
        _shader.SetChangesToMaterial(faceMat);
    }
}