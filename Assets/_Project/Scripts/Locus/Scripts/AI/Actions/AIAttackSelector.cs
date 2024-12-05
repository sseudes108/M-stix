using System.Collections;
using UnityEngine;
public class AIAttackSelector : AIAction {
    public AIAttackSelector(AI ai, AIActor actor) {
        _AI = ai;
        _Actor = actor;
    }

    public IEnumerator SelectAttackRoutine(){
        _Actor.FieldChecker.OrganizeAIMonsterCardsOnField(_Actor.CardOrganizer.AIMonstersOnField);

        yield return new WaitForSeconds(1);

        if(_Actor.FieldChecker.AIMonstersOnFieldThatCanAttack.Count > 0){
            OrganizeAIMonstersByAttack();

            if(_Actor.CardOrganizer.PlayerMonstersOnField.Count > 0){
                CheckMonstersToBattle(0, 0);
            }else{
                if(_Actor.CardOrganizer.PlayerArcanesOnField.Count > 0){

                }else{
                    //Direct Attack
                }
            }

            /*
                Organize Player Monsters By Atk, Def and Lvl
                Count the star gods from AI field to implement or decrement the attack of AI monsters
                (Can destoy the strongest monster in Attack?){
                    (any arcanes on field?){
                        //random choice to make the attack in defense with the second strongest monster or not
                    }else{
                        //Attack player monster in attack
                    }
                }else{
                    (any arcanes on field?){
                        //random choice to make the attack the monster in attack with the second strongest monster or not
                    }else{
                        //Attack player monster in defense 
                    }
                }
            */
            
            /*
                (Any arcane on field?){
                    //random choice to make an direct attack with the second strongest monster or not
                }else{
                    //Direct attack
                }
            */
        }else{
            Debug.Log($"AIMonstersThatCanAttack.Count {_Actor.FieldChecker.AIMonstersOnFieldThatCanAttack.Count}");
            Debug.LogWarning("Action End");

            _Actor.ResetAttackingMonster();
            _Actor.ActionEnd();
        }

        yield return null;
    }

    private void OrganizeAIMonstersByAttack(){
        _Actor.FieldChecker.AIMonstersOnFieldThatCanAttack.Sort((x,y) => y.Attack.CompareTo(x.Attack));
    }

    private void OrganizePlayerMonstersByAttack(){
        _Actor.CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Attack.CompareTo(x.Attack));
    }

    private void OrganizePlayerMonstersByDeffense(){
        _Actor.CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Deffense.CompareTo(x.Deffense));
    }

    private void OrganizePlayerMonstersByLevel(){
        _Actor.CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Level.CompareTo(x.Level));
    }

    private void CheckMonstersToBattle(int aiIndexCard, int playerIndexCard){
        if(_Actor.CardOrganizer.PlayerMonstersOnField[playerIndexCard]){
            CheckAnimas(_Actor.FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard], _Actor.CardOrganizer.PlayerMonstersOnField[playerIndexCard]);
            if (_Actor.FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard].Attack > _Actor.CardOrganizer.PlayerMonstersOnField[playerIndexCard].Attack){ //Can destroy the player monster
                if (_Actor.CardOrganizer.PlayerArcanesOnField.Count > 0){

                }else{
                    SetMonstersToBattle(_Actor.FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard], _Actor.CardOrganizer.PlayerMonstersOnField[playerIndexCard]);
                }
            }else{ //Can't destroy the actual player monster
                playerIndexCard++;
                if(playerIndexCard > 4){ return; }
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