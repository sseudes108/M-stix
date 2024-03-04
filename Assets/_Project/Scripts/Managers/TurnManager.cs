using UnityEngine;

public class TurnManager : MonoBehaviour {
    [SerializeField] private int _turn;

    public void ChangeTurn(){
        _turn++;
    }

    public int GetTurn() {return _turn + 1;}

    public bool IsPlayerTurn(){
        return _turn % 2 == 0;
    }
}