using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// AIActorSO Is used to manage all the AI Actions during the battle. All the events of the actions should be invocked from here, since the actions are not monobehaviours
/// </summary>

[CreateAssetMenu(fileName = "AIActor", menuName = "Mistix/AI/Actor", order = 0)]
public class AIActorSO : ScriptableObject {

    //Actions
    public AICardSelector CardSelector { get; private set; }
    public AICardStatSelector CardStatSelector { get; private set; }
    public AIBoardPlaceSelector BoardPlaceSelector { get; private set; }
    public AIEffectSelector EffectSelector { get; private set; }
    public AIAttackSelector AttackSelector { get; private set; }


    public AIFieldChecker FieldChecker { get; private set; }
    public AIFusioner Fusioner { get; private set; }

    [field:SerializeField] public AIManagerSO AIManager { get; private set; }
    [field:SerializeField] public BoardManagerSO BoardManager { get; private set; }
    
    //Events
    [HideInInspector] public UnityEvent CardSelector_OnSelectionFinished;
    [HideInInspector] public UnityEvent CardStatSelector_OnCardStatSelectionFinished;
    [HideInInspector] public UnityEvent BoardPlaceSelector_OnBoardPlaceSelected;
    [HideInInspector] public UnityEvent EffectSelector_OnEffectSelected;
    [HideInInspector] public UnityEvent ActionPhaseEnd;

    public bool MakeABoardFusion { get; private set; }
    public Card CardOnBoardToFusion { get; private set; }

    public List<Card> CardsOnAIField { get; private set; } = new();
    public List<Card> CardsOnPlayerField { get; private set; } = new();

    public bool TESTE_ATK;
    
    private void OnEnable() {
        TESTE_ATK = true;
        
        FieldChecker ??= new(this);
        Fusioner ??= new(this);

        CardSelector ??= new(this);
        CardStatSelector ??= new(this);
        BoardPlaceSelector ??= new(this);
        EffectSelector ??= new(this);
        AttackSelector ??= new(this);

        CardSelector_OnSelectionFinished ??= new UnityEvent();
        CardStatSelector_OnCardStatSelectionFinished ??= new UnityEvent();
        EffectSelector_OnEffectSelected ??= new UnityEvent();
    }

#region End Action Signals
    public void CardSelectionFinished(){
        CardSelector_OnSelectionFinished?.Invoke();
    }

    public void CardStatSelectionFinished(){
        CardStatSelector_OnCardStatSelectionFinished?.Invoke();
    }

    public void BoardPlaceSelected(){
        BoardPlaceSelector_OnBoardPlaceSelected?.Invoke();
    }

    public void EffectSelected(){
        /*
            if there some monster on field that can attack
                enter attack select routine
            else
                ActionEnd()
        */
        if(TESTE_ATK){
            Debug.Log("TESTE_ATK = true");
            AIManager.AI.StartCoroutine(AttackSelector.SelectAttackRoutine());
            TESTE_ATK = false;
        }else{
            Debug.Log("TESTE_ATK = false");
            ActionEnd();
        }
    }

    public void ActionEnd(){
        ActionPhaseEnd?.Invoke();
    }

#endregion

#region Card Lists
    public void UpdateCardLists(List<Card> cardsInHand, List<MonsterCard> monstersOnAIField){
        FieldChecker.OrganizeCardLists(cardsInHand, monstersOnAIField);
    }

    public void SetAICardLists( List<Card> aICardsOnField, List<Card> playerCardsOnField){
        CardsOnAIField = aICardsOnField;
        CardsOnPlayerField = playerCardsOnField;
    }

    public void SetBoardPlaces(List<BoardPlace> monsterPlaces, List<BoardPlace> arcanePlaces){
        BoardPlaceSelector.SetBoardPlaces(monsterPlaces, arcanePlaces);
    }

    public void SetAIMAnager(AIManagerSO aimanager){
        AIManager = aimanager;
    }
#endregion

#region Board Fusion
    public void SetBoardFusion(Card cardToFusion){
        BoardManager.BoardFusion();
        MakeABoardFusion = true;
        CardOnBoardToFusion = cardToFusion;
    }

    public void ResetBoardFusion(){
        MakeABoardFusion = false;
        CardOnBoardToFusion = null;
    }
#endregion



}