
public abstract class AbstractState {
    public bool IsPLayerTurn { get; private set; }
    public Card ResultCard  { get; private set; }
    public int CurrentTurn { get; private set; }
    public Battle Battle { get; private set; }
    public AI AI { get; private set; }

    public abstract void Enter();
    public abstract void Exit();
    
    public void SetController(StateManager controller){
        if(controller is Battle){
            Battle = controller as Battle;
        }else if(controller is AI){
            AI = controller as AI;
        }
    }

    public void SetResultCard(){
        ResultCard = GameManager.Instance.Fusion.Fusion.ResultCard;
    }

    public void SetTurnOwner(){
        // CurrentTurn = currentTurn;
        CurrentTurn = GameManager.Instance.TurnManager.GetCurrentTurn();
        if(CurrentTurn % 2 != 0){
            IsPLayerTurn = true;
        }else{
            IsPLayerTurn = false;
        }
    }

    public virtual void SubscribeEvents(){}
    public virtual void UnsubscribeEvents(){}
}