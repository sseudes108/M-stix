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
        intensityTarget *= 2;

        do{
            var newBorderMaterial = new Material(renderer.sharedMaterials[1]);

            newBorderMaterial.SetColor("_BorderColor", newColor);
            newBorderMaterial.SetFloat("_Intensity", intensityFactor);
            renderer.materials = new[] { renderer.sharedMaterials[0], newBorderMaterial, renderer.sharedMaterials[2] };

            intensityFactor += intensityTarget/6;
            yield return new WaitForSeconds(0.05f);
        }while(intensityFactor < intensityTarget);
    }

    //Monsters
    private void LightUpAllMonsterPlaces(){
        //StartUp
        LightUpPlayerMonsterPlaces(BattleManager.Instance.ColorManager.DefaultPlayerBoardColor, 1.5f);
        LightUpEnemyMonsterPlaces(BattleManager.Instance.ColorManager.DefaultEnemyBoardColor, 1.5f);
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
        LightUpPlayerArcanePlaces(BattleManager.Instance.ColorManager.DefaultPlayerBoardColor, 1.5f);
        LightUpEnemyArcanePlaces(BattleManager.Instance.ColorManager.DefaultEnemyBoardColor, 1.5f);
    }
    private void LightUpPlayerArcanePlaces(Color color, float intensityTarget){
        ChangeArcanePlaceMaterial(_playerArcaneBoardPlaces, color, intensityTarget);
    }
    private void LightUpEnemyArcanePlaces(Color color, float intensityTarget){
        ChangeArcanePlaceMaterial(_enemyArcaneBoardPlaces, color, intensityTarget);
    }

    //Selection Place Phase

    //Reset
    public void ResetPlaceHighlightColor(float intensity){
        Color newColor = new();
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            newColor = BattleManager.Instance.ColorManager.DefaultPlayerBoardColor;
        }else{
            newColor = BattleManager.Instance.ColorManager.DefaultEnemyBoardColor;
        }

        HighlightMonsterPlaces(intensity, newColor);
        HighlightArcanePlaces(intensity, newColor);
    }

    //Highlight
    public void BoarderSelectionPhaseHighlight(Card resultCard, float intensity){
        Color newColor = new();
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            if(resultCard is CardMonster){
                newColor = BattleManager.Instance.ColorManager.PlayerMonsterBoardHighlightColor;
                HighlightMonsterPlaces(intensity, newColor);
            }else{
                newColor = BattleManager.Instance.ColorManager.PlayerArcaneBoardHighlightColor;
                HighlightArcanePlaces(intensity,newColor);
            }
        }else{
            if(resultCard is CardMonster){
                newColor = BattleManager.Instance.ColorManager.PlayerMonsterBoardHighlightColor;
                HighlightMonsterPlaces(intensity, newColor);
            }else{
                newColor = BattleManager.Instance.ColorManager.PlayerArcaneBoardHighlightColor;
                HighlightArcanePlaces(intensity,newColor);
            }
        }
    }

    private void HighlightMonsterPlaces(float intensity, Color newColor){
        List<Transform> mosterBoardPlaces;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            mosterBoardPlaces = _playerMosterBoardPlaces;
        }else{
            mosterBoardPlaces = _enemyMosterBoardPlaces;
        }
        ChangeMonsterPlaceMaterial(mosterBoardPlaces, newColor, intensity);
    }

    private void HighlightArcanePlaces(float intensity, Color newColor){
        List<Transform> arcaneBoardPlaces;
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            arcaneBoardPlaces = _playerArcaneBoardPlaces;
        }else{
            arcaneBoardPlaces = _enemyArcaneBoardPlaces;
        }
        ChangeArcanePlaceMaterial(arcaneBoardPlaces, newColor, intensity);
    }
}