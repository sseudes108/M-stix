using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private CardManagerSO _cardManager;
    [SerializeField] private TurnManagerSO _turnManager;
    
    private void Start() {
        _turnManager.ResetTurnStats();
    }

    private void UnsubscribeAllEvents(){
        
    }
}