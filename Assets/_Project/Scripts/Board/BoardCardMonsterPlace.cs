using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardCardMonsterPlace : BoardCardPlace {
    [SerializeField] private Button _changeMonsterModeButton;
    [SerializeField] private Renderer[] _renderers;
    public Renderer[] Renderers => _renderers;
    [SerializeField] private bool _canChangeMode = true;

    public static Action<BoardCardMonsterPlace> OnModeChange;

    protected override void SetMonsterCardRotation(CardMonster resultCard){
        if(resultCard.IsInAttackMode()){
            if(resultCard.IsFaceDown()){
                resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.AttackFaceDownRotation());
            }else{
                resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.AttackFaceUpRotation());
            }
        }else{
            if(resultCard.IsFaceDown()){
                resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.DefenseFaceDownRotation());
            }else{
                resultCard.RotateCard(BattleManager.Instance.BoardPlaceManager.DefenseFaceUpRotation());
            }
        }
    }

    protected override void OnMouseOver(){
        base.OnMouseOver();

        if(_cardInThisPlace != null){
            if(_cardInThisPlace.IsFaceDown()){return;}
        }

        if(_changeMonsterModeButton != null && _canChangeMode){
            _changeMonsterModeButton.gameObject.SetActive(true);
            _changeMonsterModeButton.onClick.AddListener(TriggerChangeMonsterModeEvent);
        }

        if(!_canChangeMode){
            _changeMonsterModeButton.gameObject.SetActive(false);
        }
    }
    protected override void OnMouseExit(){
        if(_changeMonsterModeButton != null){
            _changeMonsterModeButton.onClick.RemoveAllListeners();
        }
        base.OnMouseExit();
    }

    private void TriggerChangeMonsterModeEvent(){
        if(_canChangeMode == false){return;}
        OnModeChange?.Invoke(this);
        _canChangeMode = false;
    }

    public void ResetCanChangeMode(){
        _canChangeMode = true;
    }
}