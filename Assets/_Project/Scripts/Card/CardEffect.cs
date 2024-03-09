using System.Collections;
using UnityEngine;

public class CardEffect : MonoBehaviour {

    public void StartEffectRoutine(CardArcane arcaneCard, EArcaneType effectType){

        switch(effectType){
            case EArcaneType.Field:
                StartCoroutine(FieldEffect(arcaneCard));
            break;
            case EArcaneType.DamageToPlayer:
                StartCoroutine(DamageOrHealToPlayerEffect(arcaneCard));
            break;
        }
    }

    public IEnumerator FieldEffect(CardArcane arcaneCard){
        var animalink = arcaneCard.GetAnimaLink();
        (int atkMod, int defMod, int lvlMod) = arcaneCard.GetModifiers();

        var monstersOnField = BattleManager.Instance.BoardPlaceManager.GetAllMonstersOnTheField();

        foreach(var monster in monstersOnField){
            if(monster.GetAnima() == animalink){
                (int atkMonster, int defMonster, int lvlMonster) = monster.GetMonsterStats();

                int newAtk = atkMonster + atkMod;
                int newDef = defMonster + defMod;
                int newLvl = lvlMonster + lvlMod;

                monster.ChangeMonsterStats(newAtk, newDef, newLvl);
                Debug.Log($"{monster.name} - {monster.GetAnima()} / {atkMod} - {newAtk} / {defMod} - {newDef} / {lvlMod} - {newLvl}");
            }
        }

        DissolveEffectCard(arcaneCard);

        yield return new WaitForSeconds(1.1f);
        BattleManager.Instance.BoardManager.ChangeBattleFieldBackground(arcaneCard.Ilustration);
    }

    public IEnumerator DamageOrHealToPlayerEffect(CardArcane arcaneCard){
        DissolveEffectCard(arcaneCard);
        yield return new WaitForSeconds(1.1f);

        var amount = arcaneCard.GetHealOrDamageAmount();

        if(arcaneCard.IsPlayerCard()){
            if(arcaneCard.IsDamageCard()){
                BattleManager.Instance.HealthManager.DamageEnemy(amount);
            }else{
                BattleManager.Instance.HealthManager.HealPlayer(amount);
            }
        }else{
            if(arcaneCard.IsDamageCard()){
                BattleManager.Instance.HealthManager.DamagePlayer(amount);
            }else{
                BattleManager.Instance.HealthManager.HealEnemy(amount);
            }
        }
    }


    private void DissolveEffectCard(Card card){
        StartCoroutine(DissolveEffectCardRoutine(card));
    }

    private IEnumerator DissolveEffectCardRoutine(Card card){
        BattleManager.Instance.BoardPlaceManager.RemoveCardFromBoard(card);

        card.MoveCard(BattleManager.Instance.CardManager.CardEffectPosition);
        
        yield return new WaitForSeconds(0.9f);
        card.Shader.DissolveCard(Color.green);

        yield return new WaitForSeconds(0.9f);
        card.DestroyCard();
    }
}