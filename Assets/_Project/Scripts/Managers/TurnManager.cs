using UnityEngine;

public class TurnManager : MonoBehaviour {
    [SerializeField] private int _turn;

    private void Start() {
        BattleManager.Instance.UIBattleManager.UpdateTurn(_turn+1, IsPlayerTurn());
    }

    public void EndTurn(){
        _turn++;
        BattleManager.Instance.UIBattleManager.UpdateTurn(_turn+1, IsPlayerTurn());
    }

    public int GetTurn() {return _turn + 1;}

    public bool IsPlayerTurn(){
        return _turn % 2 == 0;
    }
}