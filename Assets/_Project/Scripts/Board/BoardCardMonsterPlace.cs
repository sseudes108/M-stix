using UnityEngine;

public class BoardCardMonsterPlace : BoardCardPlacement {
    [SerializeField] private Renderer[] _renderers;
    public Renderer[] Renderers => _renderers;

    protected override void SetMonsterCardRotation(CardMonster resultCard){
        if(resultCard.IsInAttackMode()){
            if(resultCard.IsFaceDown()){
                resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.AttackFaceDownRotation);
            }else{
                resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.AttackFaceUpRotation);
            }
        }else{
            if(resultCard.IsFaceDown()){
                resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.DefenseFaceDownRotation);
            }else{
                resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.DefenseFaceUpRotation);
            }
        }
    }
}