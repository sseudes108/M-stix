using System;

public class CardStatSelectPhase : AbstractState{
    public static Action<Card, bool> OnStatSelectStart;
    public override void Enter(){
        OnStatSelectStart?.Invoke(ResultCard, IsPLayerTurn);
    }

    public override void Exit(){

    }

    public override string ToString(){
        return "Card Stats Sel.";
    }
}