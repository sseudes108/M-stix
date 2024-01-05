using UnityEngine;

public class PlayerHandPositions : MonoBehaviour{
    public bool IsFree => _isFree;
    [SerializeField] private bool _isFree = true;

    public void SetPositionFree(){
        _isFree = true;
    }
    public void SetPositionOccupied(){
        _isFree = false;
    }
}
