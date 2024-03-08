using UnityEngine;

public class BoardCardArcanePlace : BoardCardPlacement {
    public override Renderer Renderer => _renderer;

    protected override void SetArcaneCardRotation(CardArcane resultCard){
        if(resultCard.IsFaceDown()){
            resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.FaceDownRotation());
        }else{
            resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.FaceUpRotation());
        }
    }
}