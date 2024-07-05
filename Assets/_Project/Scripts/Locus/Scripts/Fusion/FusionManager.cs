using UnityEngine;

[RequireComponent(typeof(FusionPositions), typeof(MonsterFusion), typeof(Fusion))]
public class FusionManager : MonoBehaviour {
    public Fusion Fusion { get; private set; }
    public FusionPositions Positions { get; private set; }
    public MonsterFusion Monster { get; private set; }

    private void Awake() {
        Fusion = GetComponent<Fusion>();
        Positions = GetComponent<FusionPositions>();
        Monster = GetComponent<MonsterFusion>();
    }
}