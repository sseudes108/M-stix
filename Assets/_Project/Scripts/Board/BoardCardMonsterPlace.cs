using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardCardMonsterPlace : BoardCardPlace {
    [SerializeField] private Button _changeMonsterModeButton;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Renderer[] _renderers;
    public Renderer[] Renderers => _renderers;
    [SerializeField] private bool _canChangeMode = true;

    public static Action<BoardCardMonsterPlace, CardMonster> OnModeChange;
    public static Action<BoardCardMonsterPlace, CardMonster> OnAttack;

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
        var monsterCard = _cardInThisPlace as CardMonster;
        var currentPhase = BattleManager.Instance.BattleStateManager.CurrentPhase;

        //Null verifications because the enemy's places does not have buttons
        if(monsterCard != null &&  currentPhase != BattleManager.Instance.AttackPhase){
            if(monsterCard.IsFaceDown()){return;}
        }
        
        if(_changeMonsterModeButton != null && _canChangeMode){
            _changeMonsterModeButton.gameObject.SetActive(true);
            _changeMonsterModeButton.onClick.AddListener(TriggerChangeMonsterModeEvent);
        }

        if(!_canChangeMode){
            _changeMonsterModeButton.gameObject.SetActive(false);
        }
        
        if(monsterCard != null && monsterCard.IsInAttackMode()){
            if(_attackButton != null){
                _attackButton.gameObject.SetActive(true);

                if(!monsterCard.IsAttacking()){
                    _attackButton.onClick.AddListener(TriggerAttackMonsterEvent);
                }
            }
        }
    }

    protected override void OnMouseExit(){
        if(_changeMonsterModeButton != null){
            _changeMonsterModeButton.onClick.RemoveAllListeners();
            _attackButton.onClick.RemoveAllListeners();
        }
        base.OnMouseExit();
    }

    protected override void OnMouseDown(){
        base.OnMouseDown();
        
        //Attack
        var currentPhase = BattleManager.Instance.BattleStateManager.CurrentPhase;
        var monsterCard = _cardInThisPlace as CardMonster;
        
        if(currentPhase == BattleManager.Instance.AttackPhase){
            if(monsterCard != null){
                if(BattleManager.Instance.TurnManager.IsPlayerTurn() && !monsterCard.IsPlayerCard()){
                    Debug.Log(monsterCard.name);
                }
            }
        }
    }

    public void ResetCanChangeMode(){
        _canChangeMode = true;
    }

    //Events
    private void TriggerChangeMonsterModeEvent(){
        if(_canChangeMode == false){return;}
        var monster = _cardInThisPlace as CardMonster;

        //Changed from attack to def
        if(monster.IsInAttackMode()){
            _attackButton.gameObject.SetActive(false);
        }

        OnModeChange?.Invoke(this, monster);
        _canChangeMode = false;
    }

    private void TriggerAttackMonsterEvent(){
        var monster = _cardInThisPlace as CardMonster;
        
        if(!monster.IsAttacking()){
            monster.SetMonsterAttacking(true);
            OnAttack?.Invoke(this, monster);
        }
    }
}