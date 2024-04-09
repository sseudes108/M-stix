using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardCardMonsterPlace : BoardCardPlace {
    [SerializeField] private Button _changeMonsterModeButton;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Renderer[] _renderers;
    public Renderer[] Renderers => _renderers;

    public bool _canChangeMode;
    public bool _canAttack;

    public static Action<BoardCardMonsterPlace, CardMonster> OnModeChange;
    public static Action<BoardCardMonsterPlace, CardMonster> OnAttack;

    protected override void Start(){
        base.Start();
        _canChangeMode = false;
    }

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

        //Disable all buttons to make sure that only enables the right ones
        if(_changeMonsterModeButton != null && _attackButton != null){
            _changeMonsterModeButton.gameObject.SetActive(false);
            _attackButton.gameObject.SetActive(false);
        }

        //Change Mode
        if(_canChangeMode && _changeMonsterModeButton != null){
            _changeMonsterModeButton.gameObject.SetActive(true);
            _changeMonsterModeButton.onClick.AddListener(TriggerChangeMonsterModeEvent);
        }

        //Attack
        if(currentPhase == BattleManager.Instance.AttackPhase || currentPhase == BattleManager.Instance.ActionBattlePhase){
            if(_canChangeMode && monsterCard != null && monsterCard.IsInAttackMode() && _attackButton != null && _canAttack){
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
            // Debug.Log($"{_cardInThisPlace.name}");
            if(monsterCard != null){
                if(BattleManager.Instance.TurnManager.IsPlayerTurn() && !monsterCard.IsPlayerCard()){
                    BattleManager.Instance.ActionsManager.ActionAttack.StartMonsterBattle(this, monsterCard);
                }
            }else{
                if(BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlaces().Count == 0){
                    BattleManager.Instance.ActionsManager.ActionAttack.DirectAttack();
                }
            }
        }
    }

    public void ResetChangeModeAndAttack(){
        _canChangeMode = true;
        _canAttack = true;
    }
    public void BlockChangeModeAndAttack(){
        _canChangeMode = false;
        _canAttack = false;
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
            BlockChangeModeAndAttack();
            monster.SetMonsterAttacking(true);
            OnAttack?.Invoke(this, monster);
        }
    }
}