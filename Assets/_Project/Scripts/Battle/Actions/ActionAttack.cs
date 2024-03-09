using UnityEngine;

public class ActionAttack : MonoBehaviour {
    private void OnEnable() {
        BoardCardMonsterPlace.OnAttack += BoardCardMonsterPlace_OnAttack;
    }

    private void OnDisable() {
        BoardCardMonsterPlace.OnAttack -= BoardCardMonsterPlace_OnAttack;
    }

    private void BoardCardMonsterPlace_OnAttack(BoardCardMonsterPlace place, CardMonster monster){
        var oponentTargets = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlaces();
        BattleManager.Instance.BoardPlaceVisuals.HighLightAttackTargetPlaces(oponentTargets);


    }
}