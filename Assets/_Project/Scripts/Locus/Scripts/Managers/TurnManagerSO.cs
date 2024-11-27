using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TurnManagerSO", menuName = "Mistix/Manager/Turn", order = 0)]
public class TurnManagerSO : ScriptableObject {
    // [HideInInspector] public UnityEvent OnTurnEnd;
    [SerializeField] private BattleManagerSO _battleManager;
    public int CurrentTurn {get; private set;} = 1;
    public bool IsPlayerTurn {get; private set;} = true;

    private void OnEnable() {
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    private void OnDisable() {
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    private void BattleManager_OnStartPhase(){
        CheckPlayerTurn();
    }

    public void EndTurn(){
        CurrentTurn++;
        // OnTurnEnd?.Invoke();
        TesterUI.Instance.UpdateTurnText(CurrentTurn.ToString(), GetOwner());
    }

    private string GetOwner(){
        string owner;
        if(CurrentTurn % 2 == 0){
            owner = "AI";
        }else{
            owner = "Player";
        }
        return owner;
    }

    private void CheckPlayerTurn(){
        if(CurrentTurn % 2 != 0){
            IsPlayerTurn = true;
        }else{
            IsPlayerTurn = false;
        }
    }

    public void ResetTurnStats(){
        CurrentTurn = 1;
        IsPlayerTurn = true;
        TesterUI.Instance.UpdateTurnText(CurrentTurn.ToString(), GetOwner());
    }
}