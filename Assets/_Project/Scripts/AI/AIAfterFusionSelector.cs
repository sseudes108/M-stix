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
        foreach(var card in faceDownMonsters){
            if(card.IsInAttackMode()){
                monstersInAttack.Add(card);
            }else{
                monstersInDefense.Add(card);
            }
        }
        foreach(var card in faceUpMonsters){
            if(card.IsInAttackMode()){
                monstersInAttack.Add(card);
            }else{
                monstersInDefense.Add(card);
            }
        }

        //Se houver monstros virados para cima
        if(faceUpMonsters.Count > 0){
            if(monstersInAttack.Count > 0){
                //Vê qual o monstro mais forte do player em campo e virado para cima
                faceUpMonsters.Sort((x, y) => y.GetAttack().CompareTo(x.GetAttack()));
                if(atk > faceUpMonsters[0].GetAttack() || atk > faceUpMonsters[1].GetAttack()){
                    return 1;
                }else{
                    return 0;
                }

            }else if(monstersInDefense.Count > 0){
                //Vê qual o monstro com def mais forte do player em campo e virado para cima
                faceUpMonsters.Sort((x, y) => y.GetDefense().CompareTo(x.GetDefense()));
                if(atk > faceUpMonsters[0].GetDefense() || atk > faceUpMonsters[1].GetDefense()){
                    return 1;
                }else{
                    return 0;
                }
            }
        }else if(faceDownMonsters.Count > 0){
            if(atk >= 3000){
                return 0;
            }else{
                return 1;
            }
        }

        //Se nenhum caso for atendido, retorna atk por padrão
        return 0;

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