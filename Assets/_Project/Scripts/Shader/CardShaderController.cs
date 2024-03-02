using System.Collections;
using UnityEngine;

public class CardShaderController : MonoBehaviour {
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected Card _card;
    [SerializeField] private Material _selectedCard;

    private void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
        _card = GetComponent<Card>();
    }

    private void Start() {
        SetCardImage();
        ResetBoarderColor();
    }

    private void SetCardImage(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", _card.Ilustration);
        
        _renderer.materials = new[]{_renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2]};
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
        
        _renderer.materials = new[]{_renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2]};
    }

    public void ResetBoarderColor(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);

        faceMat.SetColor("_SelectedBorderColor", Color.black);
        faceMat.SetFloat("_Intensity", 0);

        _renderer.materials = new[]{_renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2]};
    }
}