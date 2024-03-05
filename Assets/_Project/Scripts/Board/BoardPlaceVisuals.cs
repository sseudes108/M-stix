using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BoardPlaceVisuals : MonoBehaviour {

    private void OnEnable() {
        StartCoroutine(Bug_Fix_BattleManager_Fusion());
    }

    private void OnDisable() {
        BattleManager.Instance.Fusion.OnFusionEnd -= Fusion_OnFusionEnd;
    }

    public void Fusion_OnFusionEnd(){
        // Debug.Log("Fusion Ended - Board Place Visuals");
        var cardInSelectionPlace = BattleManager.Instance.FusionPositions.GetCardInBoardSelectionPlace();
        
        if(cardInSelectionPlace is CardMonster){
            //Glow monster
            Debug.Log("Glow monster");
            
            List<Transform> monsterBoardPlaces;
            if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
                monsterBoardPlaces = BattleManager.Instance.BoardPlaceManager.PlayerBoardPlaces.MonsterPlaces;
            }else{
                monsterBoardPlaces = BattleManager.Instance.BoardPlaceManager.EnemyBoardPlaces.MonsterPlaces;
            }

            //Renderers
            List<Renderer> renderers = new();
            foreach(var monsterBoardPlace in monsterBoardPlaces){
                var boardCardMonster = monsterBoardPlace.GetComponent<BoardCardMonsterPlace>();
                var renders = boardCardMonster.Renderers;
                renderers.InsertRange(0, renders);

                foreach(var renderer in renderers){
                    var newBorderMaterial = new Material(renderer.sharedMaterials[1]);
                    
                    //Adjust to controle the brightness of the color (HDR)
                    var newColor = Color.white;
                    float intensityFactor = 1f;
                    Color adjustedColor = new Color(
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

        }else{
            //Glow Arcane
            Debug.Log("Glow Arcane");

            List<Transform> arcaneBoardPlaces;
            if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
                arcaneBoardPlaces = BattleManager.Instance.BoardPlaceManager.PlayerBoardPlaces.ArcanePlaces;
            }else{
                arcaneBoardPlaces = BattleManager.Instance.BoardPlaceManager.EnemyBoardPlaces.ArcanePlaces;
            }

            //Renderer
            foreach(var arcaneBoardPlace in arcaneBoardPlaces){
                var boardCardArcane = arcaneBoardPlace.GetComponent<BoardCardArcanePlace>();
                var renderer = boardCardArcane.Renderer;

                var newBorderMaterial = new Material(renderer.sharedMaterials[1]);
                
                //Adjust to controle the brightness of the color (HDR)
                var newColor = Color.green;
                float intensityFactor = 1f;
                Color adjustedColor = new Color(
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
    }

    private IEnumerator Bug_Fix_BattleManager_Fusion(){
        //reference Exception BattleManager_Instance.Fusion

        yield return new WaitForEndOfFrame();
        BattleManager.Instance.Fusion.OnFusionEnd += Fusion_OnFusionEnd;
    }
}