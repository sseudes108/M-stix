using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceVisuals : MonoBehaviour {
    private List<Transform> _playerMosterBoardPlaces;
    private List<Transform> _enemyMosterBoardPlaces;
    private List<Transform> _playerArcaneBoardPlaces;
    private List<Transform> _enemyArcaneBoardPlaces;

    private void Start() {
        _playerMosterBoardPlaces = BattleManager.Instance.PlayerBoardPlaces.MonsterPlaces;
        _enemyMosterBoardPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlaces;
        _playerArcaneBoardPlaces = BattleManager.Instance.PlayerBoardPlaces.ArcanePlaces;
        _enemyArcaneBoardPlaces = BattleManager.Instance.EnemyBoardPlaces.ArcanePlaces;
    }
   
    public void LightUpBoard(){
        LightUpAllArcanePlaces();
        LightUpAllMonsterPlaces();
    }

    private void ChangeMonsterPlaceMaterial(List<Transform> monsterBoardPlaces, Color monsterColor, float intensityTarget){
        List<Renderer> renderers = new();

        foreach (var monsterBoardPlace in monsterBoardPlaces){
            var boardCardMonster = monsterBoardPlace.GetComponent<BoardCardMonsterPlace>();
            var renders = boardCardMonster.Renderers;
            renderers.InsertRange(0, renders);

            foreach (var renderer in renderers){
                LightUpBoardPlaces(renderer, monsterColor, intensityTarget);
            }
        }
    }

    private void ChangeArcanePlaceMaterial(List<Transform> arcaneBoardPlaces, Color arcaneColor, float intensityTarget){
        foreach (var arcaneBoardPlace in arcaneBoardPlaces){
            var boardCardArcane = arcaneBoardPlace.GetComponent<BoardCardArcanePlace>();
            var renderer = boardCardArcane.Renderer;
            LightUpBoardPlaces(renderer, arcaneColor, intensityTarget);
        }
    }

    private void LightUpBoardPlaces(Renderer renderer, Color newColor, float intensityTarget){
        StartCoroutine(LightUpMaterialRoutine(renderer, newColor, intensityTarget));
    }

    private IEnumerator LightUpMaterialRoutine(Renderer renderer, Color newColor, float intensityTarget){
        float intensityFactor = 0;
        // float target = 0.015f;
        intensityTarget /= 100;

        do{
            var newBorderMaterial = new Material(renderer.sharedMaterials[1]);
            //Adjust to controle the brightness of the color (HDR)
            Color adjustedColor = new(
                newColor.r * intensityFactor,
                newColor.g * intensityFactor,
                newColor.b * intensityFactor,
                newColor.a
            );
        
            newBorderMaterial.SetColor("_BorderColor", adjustedColor);
            newBorderMaterial.SetFloat("_Intensity", 1.5f);
            renderer.materials = new[] { renderer.sharedMaterials[0], newBorderMaterial, renderer.sharedMaterials[2] };

            intensityFactor += 0.001f;
            yield return new WaitForSeconds(0.03f);
        }while(intensityFactor < intensityTarget);
    }

    //Monsters
    private void LightUpAllMonsterPlaces(){
        //StartUp
        LightUpPlayerMonsterPlaces(new Color(7, 8, 104), 1.5f);
        LightUpEnemyMonsterPlaces(new Color(212, 9, 9), 1.5f);
    }
    private void LightUpPlayerMonsterPlaces(Color color, float intensityTarget){
        ChangeMonsterPlaceMaterial(_playerMosterBoardPlaces, color, intensityTarget);
    }
    private void LightUpEnemyMonsterPlaces(Color color, float intensityTarget){
        ChangeMonsterPlaceMaterial(_enemyMosterBoardPlaces, color, intensityTarget);
    }

    //Arcanes
    private void LightUpAllArcanePlaces(){
        //StartUp
        LightUpPlayerArcanePlaces(new Color(7, 8, 104), 1.5f);
        LightUpEnemyArcanePlaces(new Color(212, 9, 9), 1.5f);
    }
    private void LightUpPlayerArcanePlaces(Color color, float intensityTarget){
        ChangeArcanePlaceMaterial(_playerArcaneBoardPlaces, color, intensityTarget);
    }
    private void LightUpEnemyArcanePlaces(Color color, float intensityTarget){
        ChangeArcanePlaceMaterial(_enemyArcaneBoardPlaces, color, intensityTarget);
    }


    //Selection Place Phase
    public void BoarderSelectionPhaseHighlight(Card resultCard, float intensity){
        if(resultCard is CardMonster){
            HighlightMonsterPlaces(intensity);
        }else{
            HighlightArcanePlaces(intensity);
        }
    }

    private void HighlightMonsterPlaces(float intensity){
        List<Transform> mosterBoardPlaces;
        Color monsterBoardPlaceColor;

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            mosterBoardPlaces = _playerMosterBoardPlaces;
            monsterBoardPlaceColor = new Color(7, 8, 104);
        }else{
            mosterBoardPlaces = _enemyMosterBoardPlaces;
            monsterBoardPlaceColor = new Color(212, 9, 9);
        }
        ChangeMonsterPlaceMaterial(mosterBoardPlaces, monsterBoardPlaceColor, intensity);
    }

    private void HighlightArcanePlaces(float intensity){
        List<Transform> arcaneBoardPlaces;
        Color arcaneBoardPlaceColor;

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            arcaneBoardPlaces = _playerArcaneBoardPlaces;
            arcaneBoardPlaceColor = new Color(7, 8, 104);
        }else{
            arcaneBoardPlaces = _enemyArcaneBoardPlaces;
            arcaneBoardPlaceColor = new Color(212, 9, 9);
        }
        ChangeArcanePlaceMaterial(arcaneBoardPlaces, arcaneBoardPlaceColor, intensity);
    }
}