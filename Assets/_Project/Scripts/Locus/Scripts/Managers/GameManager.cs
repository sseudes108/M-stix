using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private CardManagerSO _cardManager;
    [SerializeField] private TurnManagerSO _turnManager;
    [SerializeField] private BoardManagerSO _boardManager;
    [SerializeField] private ColorDatabaseSO _colorDataBase;
    
    private void Start() {
        _turnManager.ResetTurnStats();
        _boardManager.BoardController.SetColor( _colorDataBase.BlueBoardColor, _colorDataBase.RedBoardColor);
    }
}