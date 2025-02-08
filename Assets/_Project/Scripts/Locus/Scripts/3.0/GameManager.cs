using UnityEngine;

namespace Mistix{
    public class GameManager : MonoBehaviour {
        [SerializeField] private ColorDatabaseSO _colorDatabase;
        [SerializeField] private BattleManager _battleManager;

        private void Start() {
            _battleManager.SetPlaceColors(_colorDatabase.BlueBoardColor, _colorDatabase.RedBoardColor);
        }
    }
}