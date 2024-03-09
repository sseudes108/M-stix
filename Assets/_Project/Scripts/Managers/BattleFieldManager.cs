using System;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour {
    [SerializeField] private bool _fieldEffectActivated;
    [SerializeField] private CardArcane _activatedField;

    private void OnEnable() {
        BoardCardPlace.OnMonsterSetOnBoard += BoardCardPlace_CheckNewMonsterOnField;
    }
    private void OnDisable() {
        BoardCardPlace.OnMonsterSetOnBoard -= BoardCardPlace_CheckNewMonsterOnField;
    }

    public void SetActivatedField(CardArcane mewFieldCard){
        if(_activatedField != null){
            _activatedField.DestroyCard();
        }

        _fieldEffectActivated = true;
        _activatedField = mewFieldCard;
        _activatedField.DisableCollider();
    }

    private void BoardCardPlace_CheckNewMonsterOnField(BoardCardPlace boardCardPlace, CardMonster newMonster){
        if(_fieldEffectActivated){
            var animalink = _activatedField.GetAnimaLink();
            (int atkMod, int defMod, int lvlMod) = _activatedField.GetModifiers();

            if(newMonster.GetAnima() == animalink){
                (int atkMonster, int defMonster, int lvlMonster) = newMonster.GetMonsterStats();

                int newAtk = atkMonster + atkMod;
                int newDef = defMonster + defMod;
                int newLvl = lvlMonster + lvlMod;

                newMonster.ChangeMonsterStats(newAtk, newDef, newLvl);
            }
        }
    }
}