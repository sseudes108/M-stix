using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceVisuals : MonoBehaviour {
    List<BoardCardMonsterPlace> _playerMonsterPlaces;
    List<BoardCardArcanePlace> _playerArcanePlaces;

    List<BoardCardMonsterPlace> _enemyMonsterPlaces;
    List<BoardCardArcanePlace> _enemyArcanePlaces;

    private void Start() {
        _playerMonsterPlaces = BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements;
        _playerArcanePlaces = BattleManager.Instance.PlayerBoardPlaces.ArcanePlacements;

        _enemyMonsterPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;
        _enemyArcanePlaces = BattleManager.Instance.EnemyBoardPlaces.ArcanePlacements;
    }

    public void HighLightAttackTargetPlaces(List<BoardCardMonsterPlace> attackTargetPlaces){
        var newColor = BattleManager.Instance.ColorManager.PlayerMonsterBoardHighlightColor;
        foreach(var place in attackTargetPlaces){
            ChangeMonsterCardBorderMaterial(place, newColor, 1.5f);
        }
    }

    #region Change Material
    private void ChangeMonsterCardBorderMaterial(BoardCardMonsterPlace monsterPlace, Color newColor, float intensityTarget){
        List<Renderer> renderers = new();
        var renders = monsterPlace.Renderers;
        renderers.InsertRange(0, renders);

        foreach (var renderer in renderers){
            LightUpBoardPlace(renderer, newColor, intensityTarget);
        }
    }

    private void ChangeArcaneCardBorderMaterial(BoardCardArcanePlace arcanePlace, Color newColor, float intensityTarget){
        var renderer = arcanePlace.Renderer;
        LightUpBoardPlace(renderer, newColor, intensityTarget);
    }

    private void LightUpBoardPlace(Renderer renderer, Color newColor, float intensityTarget){
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
    #endregion

    #region All Places StartUP
    public void LightUpAllPlaces(){
        LightUpPlayerMonsterPlaces();
        LightUpEnemyMonsterPlaces();
        LightUpPlayerArcanePlaces();
        LightUpEnemyArcanePlaces();
    }

    public void LightUpPlayerMonsterPlaces(){
        var newColor = BattleManager.Instance.ColorManager.DefaultPlayerBoardColor;
        foreach(var place in _playerMonsterPlaces){
            ChangeMonsterCardBorderMaterial(place, newColor, 1.5f);
        }
    }

    public void LightUpEnemyMonsterPlaces(){
        var newColor = BattleManager.Instance.ColorManager.DefaultEnemyBoardColor;
        foreach(var place in _enemyMonsterPlaces){
            ChangeMonsterCardBorderMaterial(place, newColor, 1.5f);
        }
    }

    public void LightUpPlayerArcanePlaces(){
        var newColor = BattleManager.Instance.ColorManager.DefaultPlayerBoardColor;
        foreach(var place in _playerArcanePlaces){
            ChangeArcaneCardBorderMaterial(place, newColor, 1.5f);
        }
    }

    public void LightUpEnemyArcanePlaces(){
        var newColor = BattleManager.Instance.ColorManager.DefaultEnemyBoardColor;
        foreach(var place in _enemyArcanePlaces){
            ChangeArcaneCardBorderMaterial(place, newColor, 1.5f);
        }
    }
    #endregion

    #region Selection Phase
    public void HighLightSelectionPhase(Card card){
        Color newColor;
        if(card is CardMonster){
            newColor = BattleManager.Instance.ColorManager.PlayerMonsterBoardHighlightColor;
            if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
                foreach(var place in _playerMonsterPlaces){
                    ChangeMonsterCardBorderMaterial(place, newColor, 3f);
                }
            }else{
                foreach(var place in _enemyMonsterPlaces){
                    ChangeMonsterCardBorderMaterial(place, newColor, 3f);
                }
            }
        }else{
            newColor = BattleManager.Instance.ColorManager.PlayerArcaneBoardHighlightColor;
            if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
                foreach(var place in _playerArcanePlaces){
                    ChangeArcaneCardBorderMaterial(place, newColor, 3f);
                }
            }else{
                foreach(var place in _enemyArcanePlaces){
                    ChangeArcaneCardBorderMaterial(place, newColor, 3f);
                }
            }
        }
    }

    public void ResetPlaceHighlightColor(){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            LightUpPlayerMonsterPlaces();
            LightUpPlayerArcanePlaces();
        }else{
            LightUpEnemyMonsterPlaces();
            LightUpEnemyArcanePlaces();
        }
    }
    #endregion
}