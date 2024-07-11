public class EnemyHand : Hand {
    public override void OnEnable() {
        base.OnEnable();
        // DrawPhase.OnEnemyDraw += DrawPhase_OnEnemyDraw;
        BattleManager.OnEnemyDraw.AddListener(BattleManager_OnEnemyDraw);
    }

    public override void OnDisable() {
        base.OnDisable();
        // DrawPhase.OnEnemyDraw -= DrawPhase_OnEnemyDraw;
        BattleManager.OnEnemyDraw.RemoveListener(BattleManager_OnEnemyDraw);
    }

    private void BattleManager_OnEnemyDraw(){
        Draw();
    }
}