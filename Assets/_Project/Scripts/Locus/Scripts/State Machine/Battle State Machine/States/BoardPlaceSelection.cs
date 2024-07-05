using System;

public class BoardPlaceSelection : AbstractState{   

    public static Action<Card, bool> OnBoardPlaceSelectionStart;

    public override void Enter(){
        OnBoardPlaceSelectionStart?.Invoke(ResultCard, IsPLayerTurn);
    }

    public override void Exit(){

    }

    public override string ToString(){
        return "Board Place Sel.";
    }
}