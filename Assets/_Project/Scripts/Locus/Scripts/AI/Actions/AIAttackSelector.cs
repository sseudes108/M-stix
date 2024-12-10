using System.Collections;
using System.Collections.Generic;

public class AIAttackSelector : AIAction {
    public AIAttackSelector(AI ai, AIActor actor, AIFieldChecker fieldChecker, AICardOrganizer cardOrganizer) {
        _AI = ai;
        _Actor = actor;
        _FieldChecker = fieldChecker;
        _CardOrganizer = cardOrganizer;
    }

    private bool _targetInAttack;
    private List<MonsterCard> _playerListToAttack = new();

    public IEnumerator SelectAttackRoutine(){
        _FieldChecker.OrganizeAIMonsterCardsOnField(_CardOrganizer.AIMonstersOnField);
        _Actor.BoardManager.BoardController.ResetPlayerBoardOnList();

        if(_FieldChecker.AIMonstersOnFieldThatCanAttack.Count > 0){
            OrganizeAIMonstersByAttack();

            if(_CardOrganizer.PlayerMonstersOnField.Count > 0){// Player has monsters on field

                if(_CardOrganizer.PlayerMonstersOnFieldInAttack.Count > 0){
                    OrganizePlayerMonstersByAttack();
                    _targetInAttack = true;
                }else{
                    OrganizePlayerMonstersByDeffense();
                    _targetInAttack = false;
                }

                _playerListToAttack.Clear();
                if(_targetInAttack){
                    _playerListToAttack = _CardOrganizer.PlayerMonstersOnFieldInAttack;
                }else{
                    _playerListToAttack = _CardOrganizer.PlayerMonstersOnFieldInDeffense;
                }

                foreach(var playerMonster in _playerListToAttack){
                    CheckAnimas(_FieldChecker.AIMonstersOnFieldThatCanAttack[0], playerMonster);
                    int value = 0;

                    if(_targetInAttack){
                        value = playerMonster.Attack;
                    }else{
                        value = playerMonster.Deffense;
                    }

                    if(_FieldChecker.AIMonstersOnFieldThatCanAttack[0].Attack > value){
                        SetMonstersToBattle(_FieldChecker.AIMonstersOnFieldThatCanAttack[0], playerMonster);
                        break;
                    }
                }

                //Change Battle pahase to battle

            }else{ // direct attack
                do{
                    _FieldChecker.OrganizeAIMonsterCardsOnField(_CardOrganizer.AIMonstersOnField);
                    //direct attack;
                }while(_FieldChecker.AIMonstersOnFieldThatCanAttack.Count > 0);
                _Actor.ActionEnd();
            }

        }else{//No monsters that can attack
            _Actor.ActionEnd();
        }

        yield return null;
    }


    private void OrganizeAIMonstersByAttack(){
        _FieldChecker.AIMonstersOnFieldThatCanAttack.Sort((x,y) => y.Attack.CompareTo(x.Attack));
    }

    private void OrganizePlayerMonstersByAttack(){
        _CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Attack.CompareTo(x.Attack));
    }

    private void OrganizePlayerMonstersByDeffense(){
        _CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Deffense.CompareTo(x.Deffense));
    }

    private void OrganizePlayerMonstersByLevel(){
        _CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Level.CompareTo(x.Level));
    }

    private void CheckMonstersToBattle(int aiIndexCard, int playerIndexCard){
        if(_CardOrganizer.PlayerMonstersOnField[playerIndexCard]){
            CheckAnimas(_FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard], _CardOrganizer.PlayerMonstersOnField[playerIndexCard]);
            if (_FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard].Attack > _CardOrganizer.PlayerMonstersOnField[playerIndexCard].Attack){ //Can destroy the player monster
                if (_CardOrganizer.PlayerArcanesOnField.Count > 0){

                }else{
                    SetMonstersToBattle(_FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard], _CardOrganizer.PlayerMonstersOnField[playerIndexCard]);
                }
            }else{ //Can't destroy the actual player monster
                playerIndexCard++;
                if(playerIndexCard > _CardOrganizer.PlayerMonstersOnField.Count){ return; }
                CheckMonstersToBattle(aiIndexCard, playerIndexCard);
            }
        }
    }

    private void CheckAnimas(MonsterCard aiMonster, MonsterCard playerMonster){
        switch(aiMonster.ActiveAnima){
            case EAnimaType.Venus:
                if(playerMonster.ActiveAnima == EAnimaType.Mars){
                    aiMonster.BuffAttack();
                    return;
                }

                if(playerMonster.ActiveAnima == EAnimaType.Saturn){
                    aiMonster.DebuffAttack();
                    return;
                }
            break;

            case EAnimaType.Mars:
                if(playerMonster.ActiveAnima == EAnimaType.Saturn){
                    aiMonster.BuffAttack();
                    return;
                }
                
                if(playerMonster.ActiveAnima == EAnimaType.Venus){
                    aiMonster.DebuffAttack();
                    return;
                }
            break;

            case EAnimaType.Saturn:
                if(playerMonster.ActiveAnima == EAnimaType.Jupiter){
                    aiMonster.BuffAttack();
                    return;
                }
                
                if(playerMonster.ActiveAnima == EAnimaType.Mars){
                    aiMonster.DebuffAttack();
                    return;
                }
            break;

            case EAnimaType.Jupiter:
                if(playerMonster.ActiveAnima == EAnimaType.Mercury){
                    aiMonster.BuffAttack();
                    return;
                }
                
                if(playerMonster.ActiveAnima == EAnimaType.Saturn){
                    aiMonster.DebuffAttack();
                    return;
                }
            break;

            case EAnimaType.Mercury:
                if(playerMonster.ActiveAnima == EAnimaType.Saturn){
                    aiMonster.BuffAttack();
                    return;
                }
                
                if(playerMonster.ActiveAnima == EAnimaType.Moon){
                    aiMonster.DebuffAttack();
                    return;
                }
            break;

            case EAnimaType.Sun:
                if(playerMonster.ActiveAnima == EAnimaType.Moon){
                    aiMonster.BuffAttack();
                    return;
                }
                
                if(playerMonster.ActiveAnima == EAnimaType.Jupiter){
                    aiMonster.DebuffAttack();
                    return;
                }
            break;

            case EAnimaType.Moon:
                if(playerMonster.ActiveAnima == EAnimaType.Mars){
                    aiMonster.BuffAttack();
                    return;
                }
                
                if(playerMonster.ActiveAnima == EAnimaType.Sun){
                    aiMonster.DebuffAttack();
                    return;
                }
            break;
        }
    }

    private void SetMonstersToBattle(MonsterCard aiMonster, MonsterCard playerMonster){
        _Actor.SetAttackingMonster(aiMonster);
        _Actor.SetTargetMonster(playerMonster);
    }
}