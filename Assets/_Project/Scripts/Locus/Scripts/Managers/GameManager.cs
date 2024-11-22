using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private TurnManagerSO _turnManager;
    [SerializeField] private BoardManagerSO _boardManager;
    [SerializeField] private ColorDatabaseSO _colorDataBase;
    [SerializeField] private LifePointsManagerSO _LifePointsManager;
    
    private void Start() {
        _turnManager.ResetTurnStats();

        _boardManager.BoardController.SetColor(_colorDataBase.BlueBoardColor, _colorDataBase.RedBoardColor);

        _LifePointsManager.ResetLifePoints(this);
    }
}