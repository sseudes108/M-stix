public class EnemyHand : Hand {
    public override void OnEnable() {
        base.OnEnable();
        DrawPhase.OnEnemyDraw += DrawPhase_OnEnemyDraw;
    }

    public override void OnDisable() {
        base.OnDisable();
        DrawPhase.OnEnemyDraw -= DrawPhase_OnEnemyDraw;
    }

    private void DrawPhase_OnEnemyDraw(){
        Draw();
    }
}