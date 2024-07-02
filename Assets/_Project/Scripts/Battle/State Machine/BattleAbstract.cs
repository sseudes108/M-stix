public abstract class BattleAbstract{
    protected float _waitTime = 1f;
    protected int _currentTurn;
    protected bool _playerTurn;
    public abstract void EnterState();
    public abstract void Update();
    public abstract void ExitState();

    public void SetTurn(){
        _currentTurn = BattleManager.Instance.TurnManager.GetTurn();
        _playerTurn = BattleManager.Instance.TurnManager.IsPlayerTurn();
    }
}