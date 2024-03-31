using UnityEngine;

public class InputManager : MonoBehaviour{
    private bool _canClick = true;

    public void BlockClickInput(){_canClick = false;}
    public void AllowClickInput(){_canClick = true;}

    public bool CanClick => _canClick;
}
