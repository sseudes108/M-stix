using System.Collections;
using UnityEngine;

public class AIStateCardSelection : AIAbstract
{
    public override void EnterState(){
        BattleManager.Instance.AIManager.CardSelector.StartCardSelection();
    }

    public override void ExitState(){
        
    }

    public override void UpdateState(){
        
    }
}