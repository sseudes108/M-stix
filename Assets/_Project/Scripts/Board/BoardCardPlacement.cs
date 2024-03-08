using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardCardPlacement : MonoBehaviour {
    [SerializeField] protected bool _isFree;
    protected Collider _collider;
    protected Renderer _renderer;

    protected void Awake() {
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void Start() {
        SetPlaceFree();
    }
    
    private void OnMouseOver() {
        //Change ilustration in the ui hold card when the card itself has the collider off;
        if(BattleManager.Instance.BattleStateManager.CurrentPhase != BattleManager.Instance.BoardPlaceSelectionPhase){return;}
        if(!_isFree){
            BattleManager.Instance.UIBattleManager.UICardPlaceHolder.ChangeIllustration(GetComponentInChildren<Card>().Ilustration);
        }
    }
    
    private void OnMouseDown() {
        var resultCard = BattleManager.Instance.FusionManager.GetResultCard();

        if(BattleManager.Instance.BattleStateManager.CurrentPhase != BattleManager.Instance.BoardPlaceSelectionPhase){return;}

        if(_isFree){
            //is monster place and monster card, or is arcane place and arcane card
            if(this is BoardCardMonsterPlace && resultCard is CardMonster /* OR */
                || this is BoardCardArcanePlace && resultCard is CardArcane){

                SetCardInPlace(resultCard);
            }
        }else
        {
            //Fusion with the monster on place
            // Debug.Log("T");
            // List<Card> fusionList = new(){
            //     GetComponentInChildren<Card>(),
            //     resultCard,
            // };

            // SetPlaceFree();
            // BattleManager.Instance.FusionManager.SetFusionList(fusionList);
            // BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);

            BoardFusion(resultCard);
        }
    }

    public void BoardFusion(Card resultCard){
        Debug.Log("T");
        List<Card> fusionList = new(){
                GetComponentInChildren<Card>(),
                resultCard,
            };

        SetPlaceFree();
        BattleManager.Instance.FusionManager.SetFusionList(fusionList);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.FusionPhase);
    }

    public void SetCardInPlace(Card resultCard){
        StartCoroutine(MoveCardRoutine(resultCard));
    }

    private IEnumerator MoveCardRoutine(Card resultCard){
        if(resultCard.IsOnField() == false){;
            resultCard.MoveCard(transform);

            if(resultCard is CardMonster){
                SetMonsterCardRotation(resultCard as CardMonster);
            }else{
                SetArcaneCardRotation(resultCard as CardArcane);
            }

            resultCard.SetCardOnField();
            SetPlaceOcuppied();


            yield return new WaitForSeconds(1f);
            BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.ActionPhase);
        }
    }

    public void SetPlaceFree(){_isFree = true;}
    public void SetPlaceOcuppied(){_isFree = false;}
    public bool IsFree() => _isFree;

    public virtual Renderer Renderer => _renderer;
    protected virtual void SetMonsterCardRotation(CardMonster resultCard){}
    protected virtual void SetArcaneCardRotation(CardArcane resultCard){}

    public void DisableCardColliderInBoardPhaseSelection(){
        var card = GetComponentInChildren<Card>();
        if(card != null){
            card.DisableCollider();
        }
    }
    public void EnableCardColliderInBoardPhaseSelection(){      
        var card = GetComponentInChildren<Card>();
        if(card != null){
            card.EnableCollider();
        }
    }
}