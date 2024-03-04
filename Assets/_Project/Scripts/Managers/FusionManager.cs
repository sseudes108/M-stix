using UnityEngine;

public class FusionManager : MonoBehaviour {
    [SerializeField] private FusionPositions _fusionPositions;
    [SerializeField] private Fusion _fusion;
    [SerializeField] private FusionMonster _fusionMonster;
    [SerializeField] private FusionArcane _fusionArcane;
    [SerializeField] private FusionEquip _fusionEquip;

    private void Awake() {
        _fusion = GetComponent<Fusion>();
        _fusionPositions = GetComponent<FusionPositions>();
        _fusionMonster = GetComponent<FusionMonster>();
        _fusionArcane = GetComponent<FusionArcane>();
        _fusionEquip = GetComponent<FusionEquip>();
    }
    
    public Fusion Fusion => _fusion;
    public FusionPositions FusionPositions => _fusionPositions;
    public FusionMonster FusionMonster => _fusionMonster;
    public FusionArcane FusionArcane => _fusionArcane;
    public FusionEquip FusionEquip => _fusionEquip;
}