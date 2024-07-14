using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FusionEventHandlerSO", menuName = "Mistix/Events/Fusion", order = 0)]
public class FusionManager : ScriptableObject {
    [HideInInspector] public UnityEvent<List<Card>, bool> OnFusionStart;
    [HideInInspector] public UnityEvent<Card> OnFusionEnd;

    [HideInInspector] public UnityEvent<MonsterFusion, EMonsterType> OnCheckCardsBase;
    [HideInInspector] public UnityEvent<MonsterCard, MonsterCard> OnMonsterFusionStart;

    [HideInInspector] public UnityEvent<Card, Card, Card> OnFusionSucess;
    [HideInInspector] public UnityEvent<Card, Card> OnFusionFailed;

    public FusionPositions Positions { get; private set; }
    public Card ResultCard { get; private set; }

    private void OnEnable() {
        OnFusionStart ??= new UnityEvent<List<Card>, bool>();
        OnFusionEnd ??= new UnityEvent<Card>();

        OnMonsterFusionStart ??= new UnityEvent<MonsterCard, MonsterCard>();

        OnCheckCardsBase ??= new UnityEvent<MonsterFusion, EMonsterType>();

        OnFusionSucess ??=new UnityEvent<Card, Card, Card>();
        OnFusionFailed ??=new UnityEvent<Card, Card>();
    }

    public void CheckCardsBase(MonsterFusion fusion, EMonsterType monsterType) { OnCheckCardsBase?.Invoke(fusion, monsterType); }
    public void FusionEnd(Card ResultCard) { OnFusionEnd?.Invoke(ResultCard); }

    public void SetPositions(FusionPositions positions){
        Positions = positions;
    }

    public void SetResultedCard(Card card){
        ResultCard = card;
    }

    public void StartFusionRoutine(List<Card> selectedCards, bool isPlayerTurn){
        OnFusionStart?.Invoke(selectedCards, isPlayerTurn);
    }

    public void StartMonsterFusionRoutine(MonsterCard monster1, MonsterCard monster2){
        OnMonsterFusionStart?.Invoke(monster1, monster2);
    }

    public void FusionSucess(Card card1, Card card2, Card resultCard){
        OnFusionSucess?.Invoke(card1, card2, resultCard);
    }
    
    public void FusionFailed(Card card1, Card card2){
        OnFusionFailed?.Invoke(card1, card2);
    }
}