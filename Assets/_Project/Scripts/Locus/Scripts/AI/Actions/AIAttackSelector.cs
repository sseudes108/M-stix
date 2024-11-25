using System.Collections;
using UnityEngine;

public class AIAttackSelector : AIAction {
    public AIAttackSelector(AIActorSO actor) { _actor = actor; }

    public IEnumerator SelectAttackRoutine(){
        Debug.Log("SelectAttackRoutine()");
        yield return null;

        // if(_actor.FieldChecker.AIMonstersThatCanAttack.Count > 0){
        //     _actor.FieldChecker.AIMonstersThatCanAttack[0].SetCanAttack(false);
        // }else{

        // }

        if(_actor.MonstersOnAIFieldThatCanAttack.Count > 0){
            _actor.MonstersOnAIFieldThatCanAttack[0].SetCanAttack(false);
            _actor.AIManager.AI.ChangeState(_actor.AIManager.AI.ActionSelect);
        }else{
            
        }


    }
}