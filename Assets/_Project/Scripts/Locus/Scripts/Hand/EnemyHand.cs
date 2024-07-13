
public class EnemyHand : Hand {
    public AIManagerSO _aiManager;

    public override void OnEnable() {
        _battleManager.OnEnemyDraw.AddListener(BattleManager_OnEnemyDraw);
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    public override void OnDisable() {
        _battleManager.OnEnemyDraw.RemoveListener(BattleManager_OnEnemyDraw);
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    private void BattleManager_OnEnemyDraw(){
        _handManager.Draw(this);
        // _aiManager.SetCardsInHand()
        // .SetCardsInHand()
    }
}