// using System;

// public abstract class AbstractBattleState{
//     public static Action<AbstractBattleState> OnEnterState;
//     public static Action<AbstractBattleState> OnEndState;
//     public static Action<AIAbstract> OnAIStateChange;
//     protected float _waitTime = 1f;
//     protected float _currentTurn;
//     protected bool _isPlayerTurn;

//     public abstract void EnterState();
//     public abstract void Update();
//     public abstract void ExitState();

//     public void SetTurn(){
//         _currentTurn = BattleManager.Instance.TurnManager.GetTurn();
//         _isPlayerTurn = BattleManager.Instance.TurnManager.IsPlayerTurn();
//     }
// }