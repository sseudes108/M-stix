using UnityEngine;

public class FusionManager : MonoBehaviour {
    [SerializeField] private FusionPositions _fusionPositions;
    [SerializeField] private FusionVisuals _fusionVisuals;
    [SerializeField] private Fusion _fusion;

    public FusionPositions FusionPositions => _fusionPositions;
    public FusionVisuals FusionVisuals => _fusionVisuals;
    public Fusion Fusion => _fusion;

    private void Awake() {
        _fusionPositions = GetComponent<FusionPositions>();
        _fusionVisuals = GetComponent<FusionVisuals>();
        _fusion = GetComponent<Fusion>();
    }
}