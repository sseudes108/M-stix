using UnityEngine;
using UnityEngine.UI;

public class BoardCardArcanePlace : BoardCardPlacement {
    public override Renderer Renderer => _renderer;
    [SerializeField] private Button _activateCard;


    protected override void SetArcaneCardRotation(CardArcane resultCard){
        if(resultCard.IsFaceDown()){
            resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.FaceDownRotation());
        }else{
            resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.FaceUpRotation());
        }
    }
}