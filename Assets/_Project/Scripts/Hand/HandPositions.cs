using UnityEngine;

public class HandPositions : MonoBehaviour {
    [SerializeField] private bool _isFree;

    public void SetFree(){
        _isFree = true;
    }

    public void SetOccupied(){
        _isFree = false;
    }

    public bool Isfree() => _isFree;
}