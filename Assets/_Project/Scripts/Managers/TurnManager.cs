using System;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    public Action<bool> OnTurnEnd;
    
    [SerializeField] private int _turn;

    private void Start() {
        //First turn
        BattleManager.Instance.UIBattleManager.UpdateTurn(true);
    }

    public void EndTurn(){
        _turn++;
        OnTurnEnd?.Invoke(IsPlayerTurn());
    }

    public int GetTurn() {return _turn + 1;}

    public bool IsPlayerTurn(){
        return _turn % 2 == 0;
    }
}