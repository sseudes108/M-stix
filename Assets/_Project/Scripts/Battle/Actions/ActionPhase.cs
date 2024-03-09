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

    public void FlipCard(BoardCardPlace boardCardPlace){
        var card = boardCardPlace.GetCardInThisPlace();
        Quaternion targetRotation;

        if(card is CardMonster){
            var monster = card as CardMonster;
            if(monster.IsInAttackMode()){
                targetRotation = BattleManager.Instance.BoardPlaceManager.AttackFaceUpRotation();
            }else{
                targetRotation = BattleManager.Instance.BoardPlaceManager.DefenseFaceUpRotation();
            }
        }else{
            targetRotation = BattleManager.Instance.BoardPlaceManager.FaceUpRotation();
        }

        card.SetCardFaceUp();
        card.RotateCard(targetRotation);
    }

    public void ChangeMonsterMode(BoardCardMonsterPlace boardCardMonsterPlace){
        var card = (CardMonster)boardCardMonsterPlace.GetCardInThisPlace();
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