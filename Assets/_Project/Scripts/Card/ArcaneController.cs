using TMPro;
using UnityEngine;

public class ArcaneController : MonoBehaviour{
    public ArcaneInfo ArcaneInfo => _arcaneInfo;
    [SerializeField] private ArcaneSO _arcaneData;
    [SerializeField] private ArcaneInfo _arcaneInfo;
    [SerializeField] private SpriteRenderer _front, _ilustration;
    [SerializeField] private TMP_Text _name, _effect;

    private void Awake() {
        _arcaneInfo = _arcaneData.GetInfo();
    }

    void Start(){
        SetVisuals();
        SetTexts();
    }

    private void SetVisuals(){
        _front.sprite = ArcaneInfo.Front;
        _ilustration.sprite = ArcaneInfo.Ilustration;
    }

    private void SetTexts(){
        _name.text = ArcaneInfo.Name;
        _effect.text = ArcaneInfo.Effect;
    }
}
