public class EnemyHand : Hand {
    public override void OnEnable() {
        base.OnEnable();
        BattleManager.OnEnemyDraw.AddListener(BattleManager_OnEnemyDraw);
    }

    public override void OnDisable() {
        base.OnDisable();
        BattleManager.OnEnemyDraw.RemoveListener(BattleManager_OnEnemyDraw);
    }

    private void BattleManager_OnEnemyDraw(){
        Draw();
    }
}