using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private List<BoardPlace> _playerMonsterPlaces;
    private List<BoardPlace> _playerArcanePlaces;
    private List<BoardPlace> _enemyMonsterPlaces;
    private List<BoardPlace> _enemyArcanePlaces; 

    public void OnMonsterAttack(Card card, bool isPlayerTurn){
        Debug.Log("AttackSelected");
        if(isPlayerTurn){
            HighLightEnemyOcuppiedPlaces(card);
        }else{
            HighLightPlayerOcuppiedPlaces(card);
        }
    }

    public void OnBoardPlaceSelectionEnd(bool isPlayerTurn){
        if(isPlayerTurn){
            LightUpPlayerPlaces();
        }else{
            LightUpEnemyPlaces();
        }
    }

    public void OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn){
        if(isPlayerTurn){
            HighLightPlayerPlaces(card);
        }else{
            HighLightEnemyPlaces(card);
        }
    }

    private void HighLightPlayerOcuppiedPlaces(Card card){
        List<BoardPlace> list;

        list = card is MonsterCard ? _playerMonsterPlaces : _playerArcanePlaces;
        
        // if(card is MonsterCard){
        //     list = _playerMonsterPlaces;
        // }else{
        //     list = _playerArcanePlaces;
        // }

        foreach (var place in list.Where(place => !place.IsFree)){
            place.Visual.HighLight();
        }
        
        // foreach (BoardPlace place in list){
        //     if(!place.IsFree){
        //         place.Visual.HighLight();
        //     }
        // }
    }

    private void HighLightEnemyOcuppiedPlaces(Card card){
        List<BoardPlace> list;

        list = card is MonsterCard ? _enemyMonsterPlaces : _enemyArcanePlaces;
        
        // if(card is MonsterCard){
        //     list = _enemyMonsterPlaces;
        // }else{
        //     list = _enemyArcanePlaces;
        // }
        
        foreach (BoardPlace place in list){
            if(!place.IsFree){
                // Debug.Log("!place.IsFree");
                place.Visual.HighLight();
            }else{
                // Debug.Log("place.IsFree");
            }
        }
    }

    private void HighLightPlayerPlaces(Card card){
        if(card is MonsterCard){
            foreach (BoardPlace place in _playerMonsterPlaces){
                place.Visual.HighLight();
            }
        }else{
            foreach (BoardPlace place in _playerArcanePlaces){
                place.Visual.HighLight();
            }
        }
    }

    private void HighLightEnemyPlaces(Card card){
        if(card is MonsterCard){
            foreach (BoardPlace place in _enemyMonsterPlaces){
                place.Visual.HighLight();
            }
        }else{
            foreach (BoardPlace place in _enemyArcanePlaces){
                place.Visual.HighLight();
            }
        }
    }

    public void OnStartPhase(){
        LightUpPlayerPlaces();
        LightUpEnemyPlaces();
    }

    private void LightUpPlayerPlaces(){
        foreach (BoardPlace place in _playerMonsterPlaces){
            place.Visual.LightUp();
        }
        foreach (BoardPlace place in _playerArcanePlaces){
            place.Visual.LightUp();
        }
    }

    private void LightUpEnemyPlaces(){
        foreach (BoardPlace place in _enemyMonsterPlaces){
            place.Visual.LightUp();
        }
        foreach (BoardPlace place in _enemyArcanePlaces){
            place.Visual.LightUp();
        }
    }
}