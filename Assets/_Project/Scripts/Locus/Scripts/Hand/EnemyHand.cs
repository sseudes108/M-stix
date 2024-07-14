public class EnemyHand : Hand {
    public override void OnEnable() {
        base.OnEnable();
        BattleManager.OnPlayerDraw.AddListener(BattleManager_EnemyDraw);
    }

    public override void OnDisable() {
        base.OnDisable();
        BattleManager.OnPlayerDraw.AddListener(BattleManager_EnemyDraw);
    }

    private void BattleManager_EnemyDraw(){ Draw(); }
}