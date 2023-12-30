using System;
using UnityEngine;

public class HandAction : MonoBehaviour{
    public static Action OnFusionStarted;
    public static HandAction Instance;
    private HandCardSelection _handCardSelection;
    
    private void Awake() {
        if(Instance != null){
            Debug.Log("Error: There's more than one instance of HandAction" + Instance + this);
            Destroy(gameObject);
        }
        Instance = this;
        
        _handCardSelection = GetComponent<HandCardSelection>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            Fusion.Instance.StartFusionLine(_handCardSelection.SelectedCards);
            OnFusionStarted?.Invoke();
        }
    }
}
