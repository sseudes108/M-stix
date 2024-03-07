using UnityEngine;

public abstract class AIAbstract : MonoBehaviour {
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

}