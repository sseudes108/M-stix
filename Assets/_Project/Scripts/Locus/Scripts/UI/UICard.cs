using UnityEngine;

public class UICard : MonoBehaviour {
    [SerializeField] private UIEventHandlerSO _uIManager;
    private Renderer _renderer;
    
    private void OnEnable() {
        _uIManager.OnUpdateIllustration.AddListener(UIManager_OnUpdateIllustration);
    }

    private void OnDisable() {
        _uIManager.OnUpdateIllustration.RemoveListener(UIManager_OnUpdateIllustration);
    }

    private void Awake() {
        _renderer = transform.Find("Card").GetComponentInChildren<Renderer>();
    }

    private void UIManager_OnUpdateIllustration(Texture2D newIllustration){
        UpdateIllustration(newIllustration);
    }

    public void UpdateIllustration(Texture2D illustration){
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", illustration);

        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
    }
}