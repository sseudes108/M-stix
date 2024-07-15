using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TurnManagerSO", menuName = "Mistix/Manager/Turn", order = 0)]
public class TurnManagerSO : ScriptableObject {
    [SerializeField] private BattleManagerSO _battleManager;
    public int CurrentTurn = 1;
    public bool IsPlayerTurn {get; private set;} = true;

    [HideInInspector] public UnityEvent OnTurnEnd;

    private void OnEnable() {
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    private void OnDisable() {
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    private void BattleManager_OnStartPhase(){
        Debug.Log($"{CurrentTurn} - Before CheckPlayerTurn - BattleManager_OnStartPhase()");
        CheckPlayerTurn();
        Debug.Log($"{CurrentTurn} - After CheckPlayerTurn - BattleManager_OnStartPhase()");
    }

    public void EndTurn(){
        CurrentTurn++;
        OnTurnEnd?.Invoke();
        TesterUI.Instance.UpdateTurnText(CurrentTurn.ToString(), GetOwner());
    }

    private string GetOwner(){
        string owner;
        if(IsPlayerTurn){
            owner = "Player";
        }else{
            owner = "Enemy";
        }
        return owner;
    }

    public void CheckPlayerTurn(){
        if(CurrentTurn % 2 != 0){
            // Tester.Instance.CheckCall("TurnManagerSO ","CurrentTurn % 2 != 0", "magenta");
            IsPlayerTurn = true;
            // Tester.Instance.CheckCall("IsPlayerTurn Should be true: ",$"{IsPlayerTurn}", "magenta");
        }else{
            // Tester.Instance.CheckCall("TurnManagerSO ","CurrentTurn % 2 == 0", "magenta");
            IsPlayerTurn = false;
            // Tester.Instance.CheckCall("IsPlayerTurn Should be false: ",$"{IsPlayerTurn}", "magenta");
        }
    }

    public string GetTurnOwner(){
        if(IsPlayerTurn){
            return "Player";
        }else{
            return "Ai";
        }
    }

    public void ResetTurnStats(){
        CurrentTurn = 1;
        IsPlayerTurn = true;
        TesterUI.Instance.UpdateTurnText(CurrentTurn.ToString(), GetOwner());
    }
}