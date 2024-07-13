using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FusionEventHandlerSO", menuName = "Mistix/Events/Fusion", order = 0)]
public class FusionEventHandlerSO : ScriptableObject {
    public UnityEvent<MonsterFusion, EMonsterType> OnCheckCardsBase;
    public UnityEvent OnFusionEnd;
    
    private void OnEnable() {
        OnCheckCardsBase ??= new UnityEvent<MonsterFusion, EMonsterType>();
        OnFusionEnd ??= new UnityEvent();
    }

    public void CheckCardsBase(MonsterFusion fusion, EMonsterType monsterType) { OnCheckCardsBase?.Invoke(fusion, monsterType); }
    public void FusionEnd() { OnFusionEnd?.Invoke(); }
}