using System;
using UnityEngine;

public class ActionsManager : MonoBehaviour {
    [SerializeField] private AfterFusionSelections _afterFusionSelections;
    [SerializeField] private ActionPhase _actionPhase;
    [SerializeField] private ActionAttack _actionAttack;

    private void Awake() {
        _afterFusionSelections = GetComponent<AfterFusionSelections>();
        _actionPhase = GetComponent<ActionPhase>();
        _actionAttack = GetComponent<ActionAttack>();
    }

    public AfterFusionSelections AfterFusionSelections => _afterFusionSelections;
    public ActionPhase ActionPhase => _actionPhase;
    public ActionAttack ActionAttack => _actionAttack;
} 