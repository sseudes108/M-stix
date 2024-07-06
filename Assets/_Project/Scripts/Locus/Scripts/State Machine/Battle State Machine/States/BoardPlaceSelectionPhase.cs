using System;
public class BoardPlaceSelectionPhase : AbstractState{   

    public static Action<Card, bool> OnBoardPlaceSelectionStart;
    public static Action<Card, bool> OnBoardPlaceSelectionEnd;

    public override void Enter(){
        SubscribeEvents();
        OnBoardPlaceSelectionStart?.Invoke(ResultCard, IsPLayerTurn);
    }

    public override void Exit(){
        UnsubscribeEvents();
    }

    public override void SubscribeEvents(){
        BoardPlace.OnBoardPlaceSelected += BoardPlace_OnBoardPlaceSelected;
    }

    public override void UnsubscribeEvents(){
        BoardPlace.OnBoardPlaceSelected -= BoardPlace_OnBoardPlaceSelected;
    }

    private void BoardPlace_OnBoardPlaceSelected(){
        OnBoardPlaceSelectionEnd.Invoke(ResultCard, IsPLayerTurn);
        // Battle.ChangeState()
    }

    public override string ToString(){
        return "Board Place Sel.";
    }
}