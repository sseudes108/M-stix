using UnityEngine;

public class HandPosition : MonoBehaviour {
    public bool IsFree {get; private set;} = true;

    public void IsPlaceFree(bool isfree){
        IsFree = isfree;
    }
}