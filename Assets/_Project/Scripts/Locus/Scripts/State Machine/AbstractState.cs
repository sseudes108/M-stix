using UnityEngine;

public abstract class AbstractState {
    public Battle Battle { get; private set; }
    public AI AI { get; private set; }

    public abstract void Enter();
    public abstract void Exit();
        
    public void SetController(MonoBehaviour controller){
        if(controller is Battle){
            Battle = controller as Battle;
        }else if(controller is AI){
            AI = controller as AI;
        }
    }

    public virtual void SubscribeEvents(){}
    public virtual void UnsubscribeEvents(){}
    public Battle GetBattleController(){
        return Battle;
    }
}