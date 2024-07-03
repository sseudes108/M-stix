using UnityEngine;

public class TurnManager : MonoBehaviour {
    public int CurrentTurn {get; private set;} = 1;

    private void EndTurn(){
        CurrentTurn++;
    }

    public int GetCurrentTurn(){
        return CurrentTurn;
    }
}