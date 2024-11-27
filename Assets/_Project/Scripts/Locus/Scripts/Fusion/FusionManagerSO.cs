using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FusionManagerSO", menuName = "Mistix/Manager/Fusion", order = 0)]
public class FusionManagerSO : ScriptableObject {
    [HideInInspector] public UnityEvent<List<Card>, bool> OnFusionStart;
    [HideInInspector] public UnityEvent<Card> OnFusionEnd;
    [HideInInspector] public UnityEvent<MonsterCard, MonsterCard> OnMonsterFusionStart;

    [HideInInspector] public UnityEvent<Card, Card, Card> OnFusionSucess;
    [HideInInspector] public UnityEvent<Card, Card> OnFusionFailed;

    public FusionPositions Positions { get; private set; }
    public Card ResultCard { get; private set; }

    private void OnEnable() {
        OnFusionStart ??= new UnityEvent<List<Card>, bool>();
        OnFusionEnd ??= new UnityEvent<Card>();

        OnMonsterFusionStart ??= new UnityEvent<MonsterCard, MonsterCard>();
        OnFusionSucess ??=new UnityEvent<Card, Card, Card>();
        OnFusionFailed ??=new UnityEvent<Card, Card>();
    }

    public void SetPositions(FusionPositions positions) { Positions = positions; }
    public void SetResultCard(Card card) { ResultCard = card; }

    public void StartFusionRoutine(List<Card> selectedCards, bool isPlayerTurn) { OnFusionStart?.Invoke(selectedCards, isPlayerTurn); }
    
    public void StartMonsterFusionRoutine(MonsterCard monster1, MonsterCard monster2) { OnMonsterFusionStart?.Invoke(monster1, monster2); }

    public void FusionSucess(Card card1, Card card2, Card resultCard) { OnFusionSucess?.Invoke(card1, card2, resultCard); }
    public void FusionFailed(Card card1, Card card2) { OnFusionFailed?.Invoke(card1, card2); }

    public void FusionEnd(Card ResultCard) { OnFusionEnd?.Invoke(ResultCard); }
}