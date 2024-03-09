using UnityEngine;

public class ActionsManager : MonoBehaviour {
    [SerializeField] private AfterFusionSelections _afterFusionSelections;
    [SerializeField] private ActionPhase _actionPhase;

    private void Awake() {
        _afterFusionSelections = GetComponent<AfterFusionSelections>();
        _actionPhase = GetComponent<ActionPhase>(); 
    }

    public AfterFusionSelections AfterFusionSelections => _afterFusionSelections;
    public ActionPhase ActionPhase => _actionPhase;
} 