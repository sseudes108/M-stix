using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FusionEventHandlerSO", menuName = "Mistix/Events/Fusion", order = 0)]
public class FusionEventHandlerSO : ScriptableObject {
    [HideInInspector] public UnityEvent<MonsterFusion, EMonsterType> OnCheckCardsBase;
    [HideInInspector] public UnityEvent<Card> OnFusionEnd;
    
    public FusionPositions Positions { get; private set; }
    public Card resultedCard;

    private void OnEnable() {
        OnCheckCardsBase ??= new UnityEvent<MonsterFusion, EMonsterType>();
        OnFusionEnd ??= new UnityEvent<Card>();
    }

    public void CheckCardsBase(MonsterFusion fusion, EMonsterType monsterType) { OnCheckCardsBase?.Invoke(fusion, monsterType); }
    public void FusionEnd(Card ResultCard) { OnFusionEnd?.Invoke(ResultCard); }
    public void SetPositions(FusionPositions positions){
        Positions = positions;
    }

    public void SetResultedCard(Card card){
        resultedCard = card;
    }
}