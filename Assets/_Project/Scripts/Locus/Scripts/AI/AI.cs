using UnityEngine;

public class AI : MonoBehaviour {
    public AIManagerSO Manager;
    public BattleManagerSO BattleManager;
    public AIActorSO Actor;

    private void Start(){
        Manager.SetAI(this);
    }
}