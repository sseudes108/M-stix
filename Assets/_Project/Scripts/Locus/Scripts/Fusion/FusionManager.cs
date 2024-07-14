using UnityEngine;

public class FusionManager : MonoBehaviour {
    public Fusion Fusion { get; private set; }
    // public FusionPositions Positions { get; private set; }
    public MonsterFusion Monster { get; private set; }

    private void Awake() {
        // Positions ??= new();
        Fusion = GetComponent<Fusion>();
        Monster = GetComponent<MonsterFusion>();
    }
}