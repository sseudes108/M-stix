using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceVisuals : MonoBehaviour {

    public void Fusion_OnFusionEnd(Card fusionResultCard){

        var isPlayerTurn = BattleManager.Instance.TurnManager.IsPlayerTurn();

        if(fusionResultCard is CardMonster){
            //Glow Monster places

            List<Transform> monsterBoardPlaces;
            if(isPlayerTurn){
                monsterBoardPlaces = BattleManager.Instance.PlayerBoardPlaces.MonsterPlaces;
            }else{
                monsterBoardPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlaces;
            }

            //Renderers
            List<Renderer> renderers = new();
            foreach(var monsterBoardPlace in monsterBoardPlaces){
                var boardCardMonster = monsterBoardPlace.GetComponent<BoardCardMonsterPlace>();
                var renders = boardCardMonster.Renderers;
                renderers.InsertRange(0, renders);

                foreach(var renderer in renderers){
                    var newColor = Color.white;
                    
                    ChangeMaterial(renderer, newColor);
                }
            }

        }else{
            //Glow Arcane places

            List<Transform> arcaneBoardPlaces;
            if(isPlayerTurn){
                arcaneBoardPlaces = BattleManager.Instance.PlayerBoardPlaces.ArcanePlaces;
            }else{
                arcaneBoardPlaces = BattleManager.Instance.EnemyBoardPlaces.ArcanePlaces;
            }

            //Renderer
            foreach(var arcaneBoardPlace in arcaneBoardPlaces){
                var boardCardArcane = arcaneBoardPlace.GetComponent<BoardCardArcanePlace>();
                var renderer = boardCardArcane.Renderer;

                var newColor = Color.green;
                ChangeMaterial(renderer, newColor);
            }
        }
    }

    private static void ChangeMaterial(Renderer renderer, Color newColor){
        var newBorderMaterial = new Material(renderer.sharedMaterials[1]);

        //Adjust to controle the brightness of the color (HDR)
        float intensityFactor = 1f;
        Color adjustedColor = new(
            newColor.r * intensityFactor,
            newColor.g * intensityFactor,
            newColor.b * intensityFactor,
            newColor.a
        );

        newBorderMaterial.SetColor("_BorderColor", adjustedColor);
        newBorderMaterial.SetFloat("_Intensity", 10f);
        newBorderMaterial.SetFloat("_TimeMultiplier", 10f);

        renderer.materials = new[] { renderer.sharedMaterials[0], newBorderMaterial, renderer.sharedMaterials[2] };
    }
}