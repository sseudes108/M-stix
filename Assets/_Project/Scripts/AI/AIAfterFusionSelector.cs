using UnityEngine;

public class AIAfterFusionSelector : MonoBehaviour {
    public int AnimaSelection(){
        //Seleção aleatoria
        return Random.Range(0,2);
    }

    public int MonsterModeSelection(){
        //Seleção aleatoria
        // 0 = atak 1 = def
        // return Random.Range(0,2);
        return 1;
    }

    public int FaceSelection(){
        //Seleção aleatoria
        // 0 = faceUp 1 = faceDown
        // return Random.Range(0,2);
        return 0;
    }
}