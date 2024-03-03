using UnityEngine;

public class FusionManager : MonoBehaviour {
    [SerializeField] private FusionPositions _fusionPositions;
    [SerializeField] private FusionVisuals _fusionVisuals;

    [SerializeField] private Fusion _fusion;
    [SerializeField] private FusionMonster _fusionMonster;
    [SerializeField] private FusionArcane _fusionArcane;
    [SerializeField] private FusionEquip _fusionEquip;


    public FusionPositions FusionPositions => _fusionPositions;
    public FusionVisuals FusionVisuals => _fusionVisuals;

    public Fusion Fusion => _fusion;
    public FusionMonster FusionMonster => _fusionMonster;
    public FusionArcane FusionArcane => _fusionArcane;
    public FusionEquip FusionEquip => _fusionEquip;

    private void Awake() {
        _fusionPositions = GetComponent<FusionPositions>();
        _fusionVisuals = GetComponent<FusionVisuals>();

        _fusion = GetComponent<Fusion>();

        _fusionMonster = GetComponent<FusionMonster>();
        _fusionArcane = GetComponent<FusionArcane>();
        _fusionEquip = GetComponent<FusionEquip>();
    }
}