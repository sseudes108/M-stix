using TMPro;
using UnityEngine;

public class ArcaneCard : MonoBehaviour{
    public Arcane ArcaneInfo => _arcaneInfo;
    [SerializeField] private CardSO _cardData;
    [SerializeField] private SpriteRenderer _frameSpriteRenderer, _illustrationSpriteRenderer;
    [SerializeField] TMP_Text _name, _effect;
    private  Arcane _arcaneInfo;
    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();
        _arcaneInfo = _cardData.GetArcaneInfo();
    }

    private void Start() {
        SetCard(_arcaneInfo);
    }
    
    private void SetCard(Arcane arcaneInfo){
        _name.text = arcaneInfo.Name;
        _effect.text = arcaneInfo.Effect;
        _frameSpriteRenderer.sprite = arcaneInfo.Frame;
        _illustrationSpriteRenderer.sprite = arcaneInfo.Illustration;
    }
}
