using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// AIActorSO Is used to manage all the Ai Actions during the battle. All the events of the actions should be invocked from here, since the actions are not monobehaviours
/// </summary>

[CreateAssetMenu(fileName = "AIActor", menuName = "Mistix/AI/Actor", order = 0)]
public class AIActorSO : ScriptableObject {

    //Actions
    public AICardSelector CardSelector { get; private set; }
    public AICardStatSelector CardStatSelector { get; private set; }
    public AIBoardPlaceSelector BoardPlaceSelector { get; private set; }
    public AIFieldChecker FieldChecker { get; private set; }
    public AIFusioner Fusioner { get; private set; }

    [field:SerializeField] public AIManagerSO AIManager { get; private set; }
    [field:SerializeField] public BoardManagerSO BoardManager { get; private set; }
    
    //Events
    [HideInInspector] public UnityEvent CardSelector_OnSelectionFinished;
    [HideInInspector] public UnityEvent CardStatSelector_OnCardStatSelectionFinished;
    [HideInInspector] public UnityEvent BoardPlaceSelector_OnBoardPlaceSelected;

    public bool MakeABoardFusion { get; private set; }
    public Card CardOnBoardToFusion { get; private set; }

    public List<Card> CardsOnAIField { get; private set; } = new();
    public List<Card> CardsOnPlayerField { get; private set; } = new();
    
    private void OnEnable() {
        FieldChecker ??= new(this);
        Fusioner ??= new(this);

        CardSelector ??= new(this);
        CardStatSelector ??= new(this);
        BoardPlaceSelector ??= new(this);

        CardSelector_OnSelectionFinished ??= new UnityEvent();
        CardStatSelector_OnCardStatSelectionFinished ??= new UnityEvent();
    }

    public void CardSelectionFinished(){
        CardSelector_OnSelectionFinished?.Invoke();
    }

    public void CardStatSelectionFinished(){
        CardStatSelector_OnCardStatSelectionFinished?.Invoke();
    }

    public void BoardPlaceSelected(){
        BoardPlaceSelector_OnBoardPlaceSelected?.Invoke();
    }

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

    public void SetBoardFusion(Card cardToFusion){
        BoardManager.BoardFusion();
        MakeABoardFusion = true;
        CardOnBoardToFusion = cardToFusion;
    }

    public void ResetBoardFusion(){
        MakeABoardFusion = false;
        CardOnBoardToFusion = null;
    }
}