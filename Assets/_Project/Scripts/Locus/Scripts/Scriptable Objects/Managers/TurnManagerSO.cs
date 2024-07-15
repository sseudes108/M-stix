using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TurnManagerSO", menuName = "Mistix/Manager/Turn", order = 0)]
public class TurnManagerSO : ScriptableObject {
    public int CurrentTurn = 1;
    public bool _isPlayerTurn = true;

    [HideInInspector] public UnityEvent OnTurnEnd;

    private void OnDisable() {
        ResetTurnStats();
    }

    public void EndTurn(){
        CurrentTurn++;
        OnTurnEnd?.Invoke();
    }

    public bool IsPlayerTurn(){
        if(CurrentTurn % 2 != 0){
            _isPlayerTurn = true;
        }else{
            _isPlayerTurn = false;
        }
        return _isPlayerTurn;
    }

    public string GetTurnOwner(){
        if(IsPlayerTurn()){
            return "Player";
        }else{
            return "Ai";
        }
    }

    private void ResetTurnStats(){
        CurrentTurn = 1;
        _isPlayerTurn = true;
    }
}