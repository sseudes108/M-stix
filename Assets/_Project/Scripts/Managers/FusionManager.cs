using UnityEngine;

public class FusionManager : MonoBehaviour {
    [SerializeField] private FusionPositions _fusionPositions;
    [SerializeField] private Fusion _fusion;

    public FusionPositions FusionPositions => _fusionPositions;
    public Fusion Fusion => _fusion;

    private void Awake() {
        _fusionPositions = GetComponent<FusionPositions>();
        _fusion = GetComponent<Fusion>();
    }
}