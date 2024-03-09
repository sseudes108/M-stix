using UnityEngine;

public class ActionPhase : MonoBehaviour {

    private void OnEnable() {
        BoardCardMonsterPlace.OnModeChange += ChangeMonsterMode;
        BoardCardPlace.OnFlipCard += FlipCard;
    }

    private void OnDisable() {
        BoardCardMonsterPlace.OnModeChange -= ChangeMonsterMode;
        BoardCardPlace.OnFlipCard -= FlipCard;
    }

    public void FlipCard(BoardCardPlace boardCardPlace, Card card){
        Quaternion targetRotation;

        if(card is CardMonster){
            var monster = card as CardMonster;
            if(monster.IsInAttackMode()){
                targetRotation = BattleManager.Instance.BoardPlaceManager.AttackFaceUpRotation();
            }else{
                targetRotation = BattleManager.Instance.BoardPlaceManager.DefenseFaceUpRotation();
            }
            card.RotateCard(targetRotation);
            card.SetCardFaceUp();
        }else{
            var arcaneCard = card as CardArcane;
            BattleManager.Instance.CardEffect.ActiveCardEffect(arcaneCard);
            card.SetCardFaceUp();
        }
    }

    public void ChangeMonsterMode(BoardCardMonsterPlace boardCardMonsterPlace, CardMonster card){
        Quaternion targetRotation;
        if(card.IsInAttackMode()){
            targetRotation = BattleManager.Instance.BoardPlaceManager.DefenseFaceUpRotation();
            card.SetDefenseMode();
        }else{
            targetRotation = BattleManager.Instance.BoardPlaceManager.AttackFaceUpRotation();
            card.SetAttackMode();
        }

        card.RotateCard(targetRotation);
    }
}