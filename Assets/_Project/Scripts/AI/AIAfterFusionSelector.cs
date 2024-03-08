using UnityEngine;

public class AIAfterFusionSelector : MonoBehaviour {
    public int AnimaSelection(){
        //Seleção aleatoria
        return Random.Range(0,2);
    }

    public int MonsterModeSelection(){
        //Seleção aleatoria
        return Random.Range(0,2);
    }

    public int FaceSelection(){
        //Seleção aleatoria
        return Random.Range(0,2);
    }
}