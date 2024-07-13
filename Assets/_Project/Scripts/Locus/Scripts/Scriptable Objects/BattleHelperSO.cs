using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleHelperSO", menuName = "Mistix/Helpers/BattleHelperSO", order = 0)]
public class BattleHelperSO : ScriptableObject {
    public IEnumerator ChangeStateRoutine(float wait, AbstractState newState, Battle battle){
        Debug.Log("BattleHelperSO - ChangeStateRoutine()");
        yield return new WaitForSeconds(wait);
        battle.ChangeState(newState);
        yield return null;
    }
}