using UnityEngine;

public class HandPlacement : MonoBehaviour{
    public bool Ocuppied => _occupied;
    [SerializeField] private bool _occupied;

    public void SetOccupation(bool isOcuppied){
        _occupied = isOcuppied;
    }
}
