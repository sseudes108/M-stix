using UnityEngine;

[CreateAssetMenu(fileName = "ColorManagerSO", menuName = "Mistix/Manager/ColorSO", order = 0)]
public class ColorDatabaseSO : ScriptableObject {
    [Header("Anima")]
    public Color Mars;
    public Color Venus;
    public Color Jupiter;
    public Color Saturn;
    public Color Mercury;
    public Color Sun;
    public Color Moon;
    
    [Header("Board")]
    public Color LightUpColor;
    public Color PlayerDefaultColor;
    public Color EnemyDefaultColor;

}