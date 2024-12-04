using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIActor : MonoBehaviour {

#region Managers
    [field:SerializeField] public BoardManagerSO BoardManager { get; private set; }
    [field:SerializeField] public AIManagerSO AIManager { get; private set; }
#endregion

#region Events
    [HideInInspector] public UnityEvent CardSelector_OnSelectionFinished;
    [HideInInspector] public UnityEvent CardStatSelector_OnCardStatSelectionFinished;
    [HideInInspector] public UnityEvent BoardPlaceSelector_OnBoardPlaceSelected;
    [HideInInspector] public UnityEvent EffectSelector_OnEffectSelected;
    [HideInInspector] public UnityEvent ActionPhaseEnd;
#endregion

#region Arms
    public AICardOrganizer CardOrganizer { get; private set; }
    public AIFieldChecker FieldChecker { get; private set; }
    public AIHandChecker HandChecker { get; private set; }
    public AIFusioner Fusioner { get; private set; }
#endregion

#region Actions
    public AICardSelector CardSelector { get; private set; }
    public AICardStatSelector CardStatSelector { get; private set; }
    public AIBoardPlaceSelector BoardPlaceSelector { get; private set; }
    public AIEffectSelector EffectSelector { get; private set; }
    public AIAttackSelector AttackSelector { get; private set; }
#endregion

    public bool MakeABoardFusion { get; private set; }
    
    private Card _fusionedCard;
    public Card CardOnBoardToFusion { get; private set; }
    private AI _ai;

    public MonsterCard AttackingMonster { get; private set; }
    public MonsterCard MonsterToAttack { get; private set; }

    private void Awake() {
        _ai = GetComponent<AI>();
        CardOrganizer = GetComponent<AICardOrganizer>();
        FieldChecker = GetComponent<AIFieldChecker>();
        HandChecker = GetComponent<AIHandChecker>();

        AIManager.SetAI(_ai);
        AIManager.SetActor(this);
        
        Fusioner ??= new(_ai, this);
        CardSelector ??= new(_ai, this, HandChecker);
        BoardPlaceSelector ??= new(_ai, this);
        CardStatSelector ??= new(this);
        EffectSelector ??= new(this);
        AttackSelector ??= new(_ai, this);
    }

#region Board Fusion
    // public void IsBoardFusion(bool IsBoardFusion) { MakeABoardFusion = IsBoardFusion; }
    public void SetBoardFusion(Card cardToFusion){
        BoardManager.BoardFusion();
        MakeABoardFusion = true;
        CardOnBoardToFusion = cardToFusion;
    }

    public void ResetBoardFusion(){
        MakeABoardFusion = false;
        if(CardOnBoardToFusion != null){
            CardOnBoardToFusion.GetBoardPlace().SetPlaceFree();
            CardOnBoardToFusion = null;
        }
        AIManager.Board.ResetAIBoardOnList();
    }
#endregion

#region Fusioned Card
    public void SetFusionedCard(Card fusionedCard){
        _fusionedCard = null;
        _fusionedCard = fusionedCard;
    }

    public Card GetFusionedCard() { return _fusionedCard; }
#endregion

#region End Action Signals
    public void CardSelectionFinished() { CardSelector_OnSelectionFinished?.Invoke(); }
    public void CardStatSelectionFinished() { CardStatSelector_OnCardStatSelectionFinished?.Invoke(); }
    public void BoardPlaceSelected() { BoardPlaceSelector_OnBoardPlaceSelected?.Invoke(); }
    public void EffectSelected(){
        /*
            if there some monster on field that can attack
                enter attack select routine
            else
                ActionEnd()
        */
        FieldChecker.OrganizeAIMonsterCardsOnField(CardOrganizer.AIMonstersOnField);

        if(FieldChecker.AIMonstersOnFieldThatCanAttack.Count > 0){
            // Debug.Log($"AIMonstersThatCanAttack.Count {FieldChecker.AIMonstersOnFieldThatCanAttack.Count}");

            OrganizeAIMonstersByAttack();

            if(CardOrganizer.PlayerMonstersOnField.Count > 0){
                CheckMonstersToBattle(0, 0);
            }else{
                if(CardOrganizer.PlayerArcanesOnField.Count > 0){

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

            // AIManager.AI.StartCoroutine(AttackSelector.SelectAttackRoutine());
        }else{
            Debug.Log($"AIMonstersThatCanAttack.Count {FieldChecker.AIMonstersOnFieldThatCanAttack.Count}");
            Debug.LogWarning("Action End");
            AttackingMonster = null;
            ActionEnd();
        }
    }

    private void CheckMonstersToBattle(int aiIndexCard, int playerIndexCard){
        if(CardOrganizer.PlayerMonstersOnField[playerIndexCard]){
            CheckAnimas(FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard], CardOrganizer.PlayerMonstersOnField[playerIndexCard]);
            if (FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard].Attack > CardOrganizer.PlayerMonstersOnField[playerIndexCard].Attack){ //Can destroy the player monster
                if (CardOrganizer.PlayerArcanesOnField.Count > 0){

                }else{
                    SetMonstersToBattle(FieldChecker.AIMonstersOnFieldThatCanAttack[aiIndexCard], CardOrganizer.PlayerMonstersOnField[playerIndexCard]);
                }
            }else{ //Can't destroy the actual player monster
                playerIndexCard++;
                if(playerIndexCard == 4){ return; }
                CheckMonstersToBattle(aiIndexCard, playerIndexCard);
            }
        }
    }

    //End the turn. Called from this script when there are no monsters that can attack.
    public void ActionEnd() { ActionPhaseEnd?.Invoke(); }

#endregion
    public void OrganizeCardLists(List<Card> cardsInHand) { HandChecker.OrganizeCardsOnHand(cardsInHand); }
    
    public void SetBoardPlaces(List<BoardPlace> monsterPlaces, List<BoardPlace> arcanePlaces){
        BoardPlaceSelector.SetBoardPlaces(monsterPlaces, arcanePlaces);
    }

    private void OrganizeAIMonstersByAttack(){
        FieldChecker.AIMonstersOnFieldThatCanAttack.Sort((x,y) => y.Attack.CompareTo(x.Attack));
    }

    private void OrganizePlayerMonstersByAttack(){
        CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Attack.CompareTo(x.Attack));
    }

    private void OrganizePlayerMonstersByDeffense(){
        CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Deffense.CompareTo(x.Deffense));
    }

    private void OrganizePlayerMonstersByLevel(){
        CardOrganizer.PlayerMonstersOnField.Sort((x,y) => y.Level.CompareTo(x.Level));
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
        AttackingMonster = null;
        MonsterToAttack = null;

        AttackingMonster = aiMonster;
        MonsterToAttack = playerMonster;
    }

    public void ResetAttackPoints(){
        AttackingMonster.ResetAttack();
        MonsterToAttack.ResetAttack();
    }
}