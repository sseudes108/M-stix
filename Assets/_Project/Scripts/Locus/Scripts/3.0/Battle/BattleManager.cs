using UnityEngine;

namespace Mistix{
    
    public class BattleManager : MonoBehaviour {
        [SerializeField] private BoardManager _boardManager;

        public void LighOffAllPlaces() { _boardManager.LightOffAllPlaces(); }

        public void LightUpAllPlaces() { _boardManager.LightUpAllPlaces(); }

        public void SetPlaceColors(Vector3 blueBoardColor, Vector3 redBoardColor){
            _boardManager.SetPlaceColors(blueBoardColor, redBoardColor);
        }
    }
}