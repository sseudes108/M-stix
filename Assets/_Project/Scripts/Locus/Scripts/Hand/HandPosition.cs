using UnityEngine;

public class HandPosition : MonoBehaviour {
    public bool IsFree {get; private set;} = true;

    public void SetPlaceFree(bool isfree){
        IsFree = isfree;
    }
}