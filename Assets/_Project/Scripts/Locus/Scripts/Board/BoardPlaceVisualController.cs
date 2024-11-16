using System.Collections.Generic;

public class BoardPlaceVisualController {
    public BoardPlaceVisualController(
            List<BoardPlace> playerMonsterPlaces, 
            List<BoardPlace> playerArcanePlaces, 
            List<BoardPlace> enemyMonsterPlaces, 
            List<BoardPlace> enemyArcanePlaces
        ) {
        _playerMonsterPlaces = playerMonsterPlaces;
        _playerArcanePlaces = playerArcanePlaces;
        _enemyMonsterPlaces = enemyMonsterPlaces;
        _enemyArcanePlaces = enemyArcanePlaces;
    }

    private List<BoardPlace> _playerMonsterPlaces, _playerArcanePlaces;
    private List<BoardPlace> _enemyMonsterPlaces, _enemyArcanePlaces; 

#region Events

    public void OnStartPhase() {
        LightUpPlayerMonsterPlaces();
        LightUpEnemyMonsterPlaces();
    }

    public void OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn) {
        if(isPlayerTurn){
            HighLightPlayerMonsterPlaces();
        }else{
            HighLightEnemyMonsterPlaces();
        }
    }

    public void OnBoardPlaceSelectionEnd(bool isPlayerTurn) {
        if(isPlayerTurn){
            UnHighLightPlayerMonsterPlaces();
        }else{
            UnHighLightEnemyMonsterPlaces();
        }
    }

    public void OnMonsterAttack(bool isPlayerTurn, bool isDirectAttack) {
        if(!isDirectAttack){
            if(isPlayerTurn){
                HighLightEnemyOcuppiedMonsterPlaces();
            }else{
                HighLightPlayerOcuppiedMonsterPlaces();
            }
        }
    }

    public void OnAttackEnd(bool isPlayerTurn){
        if(isPlayerTurn){
            UnHighLightEnemyMonsterPlaces();
        }else{
            UnHighLightPlayerMonsterPlaces();
        }
    }

#endregion

#region Custom Methods

#region Player Places
    private void LightUpPlayerMonsterPlaces() {
        foreach(var place in _playerMonsterPlaces){
            place.LightUpPlace();
        }

        foreach(var place in _playerArcanePlaces){
            place.LightUpPlace();
        }
    }

    public void HighLightPlayerOcuppiedMonsterPlaces(){
        foreach(var place in _playerMonsterPlaces){
            if(place.IsFree) { continue; }
            place.HighLight();
        }
    }

    public void HighLightPlayerMonsterPlaces(){
        foreach(var place in _playerMonsterPlaces){
            place.HighLight();
        }
    }
    public void UnHighLightPlayerMonsterPlaces(){
        foreach(var place in _playerMonsterPlaces){
            place.UnHighLight();
        }
    }
#endregion

#region Enemy Places
    private void LightUpEnemyMonsterPlaces() {
        foreach(var place in _enemyMonsterPlaces){
            place.LightUpPlace();
        }
        
        foreach(var place in _enemyArcanePlaces){
            place.LightUpPlace();
        }
    }

    public void HighLightEnemyOcuppiedMonsterPlaces(){
        foreach(var place in _enemyMonsterPlaces){
            if(place.IsFree) { continue; }
            place.HighLight();
        }
    }

    public void HighLightEnemyMonsterPlaces(){
        foreach(var place in _enemyMonsterPlaces){
            place.HighLight();
        }
    }

    public void UnHighLightEnemyMonsterPlaces(){
        foreach(var place in _enemyMonsterPlaces){
            place.UnHighLight();
        }
    }
    
#endregion

#endregion
}