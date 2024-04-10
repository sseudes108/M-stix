using System.Collections.Generic;
using UnityEngine;

public class AIAfterFusionSelector : MonoBehaviour {
    public int AnimaSelection(){
        //Seleção aleatoria
        return Random.Range(0,2);
    }

    public int MonsterModeSelection(CardMonster monster){
        // 0 = atak 1 = def

        var atk = monster.GetAttack();
        var def = monster.GetDefense();

        //Pega todos os monstros do player 1 no campo
        var (faceDownMonsters, faceUpMonsters) = BattleManager.Instance.AIManager.CardSelector.GetTargetMonstersOnField();

        //Organiza os em atq e os em Def
        List<CardMonster> monstersInDefense = new();
        List<CardMonster> monstersInAttack = new();
        foreach (var card in faceDownMonsters){
            if (card.IsInAttackMode()){
                monstersInAttack.Add(card);
            }else{
                monstersInDefense.Add(card);
            }
        }
        foreach (var card in faceUpMonsters){
            if (card.IsInAttackMode()){
                monstersInAttack.Add(card);
            }else{
                monstersInDefense.Add(card);
            }
        }

        return BattleManager.Instance.AIManager.CurrentArchetype.SelectMonsterMode(atk, faceDownMonsters, faceUpMonsters, monstersInDefense, monstersInAttack);

        //Seleção aleatoria
        // return Random.Range(0,2);
    }

    public int FaceSelection(){
        //Seleção aleatoria
        // 0 = faceUp 1 = faceDown
        // return Random.Range(0,2);
        return 0;
    }
}