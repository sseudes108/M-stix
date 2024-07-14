using UnityEngine;

[CreateAssetMenu( menuName = "HelperSO")]
public class HelperSO : ScriptableObject {
    public void RedDebug(string firstText, string redText){
        Debug.Log($"{firstText}<color=red>{redText}</color=red>");
    }
    public void AllYellow(string redText){
        Debug.Log($"<color=yellow>{redText}</color=yellow>");
    }
    public void AllGreen(string redText){
        Debug.Log($"<color=green>{redText}</color=green>");
    }
}