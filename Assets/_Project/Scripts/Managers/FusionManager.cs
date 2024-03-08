using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour {
    [SerializeField] private Fusion _fusion;
    [SerializeField] private FusionMonster _fusionMonster;
    [SerializeField] private FusionArcane _fusionArcane;
    [SerializeField] private FusionEquip _fusionEquip;
    [SerializeField] private FusionAfterSelections _afterFusionSelections;
    [SerializeField] private FusionPositions _fusionPositions;
    private List<Card> _fusionList;

    private void Awake() {
        _fusion = GetComponent<Fusion>();
        _fusionPositions = GetComponent<FusionPositions>();
        _fusionMonster = GetComponent<FusionMonster>();
        _fusionArcane = GetComponent<FusionArcane>();
        _fusionEquip = GetComponent<FusionEquip>();
        _afterFusionSelections = GetComponent<FusionAfterSelections>();
    }
    
    public Fusion Fusion => _fusion;
    public FusionPositions FusionPositions => _fusionPositions;
    public FusionMonster FusionMonster => _fusionMonster;
    public FusionArcane FusionArcane => _fusionArcane;
    public FusionEquip FusionEquip => _fusionEquip;
    public FusionAfterSelections AfterFusionSelections => _afterFusionSelections;


    public void SetFusionList(){
        _fusionList = BattleManager.Instance.CardSelector.GetSelectedCards();
    }

    //On Demand Fusion (Fusion with board Cards)
    public void SetFusionList(List<Card> cardsToFusion){
        _fusionList = cardsToFusion;      
    }

    public List<Card> GetFusionList(){
        return _fusionList;
    }
}