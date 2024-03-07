using UnityEngine;

public class ColorManager : MonoBehaviour {
    [SerializeField] [ColorUsage(false, true)] private Color _defaultPlayerBoard;
    [SerializeField] [ColorUsage(false, true)] private Color _defaultEnemyBoard;
    [SerializeField] [ColorUsage(false, true)] private Color _playerMonsterBoardHighlightColor;
    [SerializeField] [ColorUsage(false, true)] private Color _playerArcaneBoardHighlightColor;
    [SerializeField] [ColorUsage(false, true)] private Color _blackColor;

    [Header("Anima Colors")]
    [SerializeField] [ColorUsage(false, true)] private Color _venus;
    [SerializeField] [ColorUsage(false, true)] private Color _mars;
    [SerializeField] [ColorUsage(false, true)] private Color _saturn;
    [SerializeField] [ColorUsage(false, true)] private Color _jupiter;
    [SerializeField] [ColorUsage(false, true)] private Color _mercury;
    [SerializeField] [ColorUsage(false, true)] private Color _sun;
    [SerializeField] [ColorUsage(false, true)] private Color _moon;

    public Color DefaultPlayerBoardColor => _defaultPlayerBoard;
    public Color DefaultEnemyBoardColor => _defaultEnemyBoard;
    public Color PlayerMonsterBoardHighlightColor => _playerMonsterBoardHighlightColor;
    public Color PlayerArcaneBoardHighlightColor => _playerArcaneBoardHighlightColor;

    public Color BlackColor => _blackColor;
    
    //Animas
    public Color Venus => _venus;
    public Color Mars => _mars;
    public Color Saturn => _saturn;
    public Color Jupiter => _jupiter;
    public Color Mercury => _mercury;
    public Color Sun => _sun;
    public Color Moon => _moon;

    public Color GetAnimaColor(EAnimaType selectedAnima){
        Color animaColor = new();
        switch(selectedAnima){
            case EAnimaType.Venus:
                animaColor = Venus;
            break;
            case EAnimaType.Mars:
                animaColor = Mars;
            break;
            case EAnimaType.Saturn:
                animaColor = Saturn;
            break;
            case EAnimaType.Jupiter:
                animaColor = Jupiter;
            break;
            case EAnimaType.Mercury:
                animaColor = Mercury;
            break;
            case EAnimaType.Sun:
                animaColor = Sun;
            break;
            case EAnimaType.Moon:
                animaColor = Moon;
            break;
            default:
                Debug.Log("Erro GetAnimaColor " + selectedAnima);
                animaColor = BlackColor;
            break;
        }
        return animaColor;
    }
}