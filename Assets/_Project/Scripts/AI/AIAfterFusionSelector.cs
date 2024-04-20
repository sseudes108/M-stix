using UnityEngine;

public class AIAfterFusionSelector : MonoBehaviour {
    public int AnimaSelection(){
        //Seleção aleatoria
        return Random.Range(0,2);
    }

    public int MonsterModeSelection(CardMonster monster){
        // 0 = atak 1 = def
        return BattleManager.Instance.AIManager.CurrentArchetype.SelectMonsterMode(monster);

        //Seleção aleatoria
        // return Random.Range(0,2);
    }

    public int FaceSelection(Card card){
        // 0 = faceUp 1 = faceDown
        return BattleManager.Instance.AIManager.CurrentArchetype.SelectCardFace(card);
        
        // Seleção aleatoria
        // return Random.Range(0,2);
    }
}