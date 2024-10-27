using UnityEngine;

public class StateMachine : MonoBehaviour {
    [field:SerializeField] public BattleManagerSO BattleManager { get; private set; }
    public Battle Battle => BattleManager.Battle;
    
    [field:SerializeField] public AIManagerSO AIManager { get; private set; }
    public AI AI => AIManager.AI;

    [field:SerializeField] public TurnManagerSO TurnManager { get; private set; }
    [field:SerializeField] public UIEventHandlerSO UIManager { get; private set; }
    [field:SerializeField] public Board Board { get; private set; }

}