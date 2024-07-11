using System;

public class CardSelectionPhase : AbstractState{
    // public static Action OnCardSelectionStart;
    // public static Action OnCardSelectionEnd;

    public override void Enter(){
        SubscribeEvents();
        Battle.BattleManager.CardSelectionStart(); // Unlock card selection
        // OnCardSelectionStart?.Invoke(); 
    }
    
    public override void Exit(){
        UnsubscribeEvents();
        Battle.BattleManager.CardSelectionEnd();// lock card selection
        // OnCardSelectionEnd?.Invoke(); // lock card selection
    }

    public override void SubscribeEvents(){
        UIBattleScene.OnSelectionFinished += UIBattleScene_OnSelectionFinished;
    }

    public override void UnsubscribeEvents(){
        UIBattleScene.OnSelectionFinished -= UIBattleScene_OnSelectionFinished;
    }

    private void UIBattleScene_OnSelectionFinished(){
        Battle.ChangeState(Battle.Fusion);
    }

    public override string ToString(){ return "Card Sel."; }
}