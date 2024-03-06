using System.Collections;
using UnityEngine;

public abstract class BoardCardPlacement : MonoBehaviour {
    protected bool _isFree;
    protected Collider _collider;
    protected Renderer _renderer;

    protected void Awake() {
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void Start() {
        _isFree = true;
    }
    
    private void OnMouseDown() {
        if(BattleManager.Instance.BattleStateManager.CurrentPhase != BattleManager.Instance.BoardPlaceSelectionPhase){return;}

        if(_isFree){
            //is monster place and monster card, or is arcane place and arcane card
            if(this is BoardCardMonsterPlace && BattleManager.Instance.Fusion.GetResultCard() is CardMonster /* OR */
                || this is BoardCardArcanePlace && BattleManager.Instance.Fusion.GetResultCard() is CardArcane){

                SetCardInPlace();

            }else{
                Debug.Log("Incorrect type");
            }

        }else{
            Debug.Log("Place is not free - Implement fusion");
        }
    }

    private void SetCardInPlace(){
        StartCoroutine(MoveCardRoutine());
    }
    private IEnumerator MoveCardRoutine(){
        if(BattleManager.Instance.Fusion.GetResultCard().IsOnField() == false){

            BattleManager.Instance.Fusion.GetResultCard().MoveCard(transform);
            BattleManager.Instance.Fusion.GetResultCard().SetCardOnField();

            yield return new WaitForSeconds(1f);
            BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.ActionPhase);
        }
    }

    public virtual Renderer Renderer => _renderer;
}