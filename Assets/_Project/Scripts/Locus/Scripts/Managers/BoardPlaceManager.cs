using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlaceManager : MonoBehaviour {
    [field:SerializeField] public float IntensityFactor{ get; private set; }
    [field:SerializeField] public Color LightUpColor{ get; private set; }
    [field:SerializeField] public Color PlayerDefaultColor{ get; private set; }
    [field:SerializeField] public Color EnemyDefaultColor{ get; private set; }

    [field:SerializeField] public List<BoardPlaceVisuals> PlayerMonsterPlaces{ get; private set; }
    [field:SerializeField] public List<BoardPlaceVisuals> PlayerArcanePlaces{ get; private set; }
    [field:SerializeField] public List<BoardPlaceVisuals> EnemyMonsterPlaces{ get; private set; }
    [field:SerializeField] public List<BoardPlaceVisuals> EnemyArcanePlaces{ get; private set; }

    private void OnEnable() {
        StartPhase.OnStartPhase += StartPhase_OnStartPhase;
        BoardPlaceSelectionPhase.OnBoardPlaceSelectionStart += BoardPlaceSelection_OnBoardPlaceSelectionStart;
        BoardPlaceSelectionPhase.OnBoardPlaceSelectionEnd += BoardPlaceSelection_OnBoardPlaceSelectionEnd;
        // UIActionPhase.OnAttackSelected += UIActionPhase_OnAttackSelected;
    }

    private void OnDisable() {
        StartPhase.OnStartPhase -= StartPhase_OnStartPhase;
        BoardPlaceSelectionPhase.OnBoardPlaceSelectionStart -= BoardPlaceSelection_OnBoardPlaceSelectionStart;
        BoardPlaceSelectionPhase.OnBoardPlaceSelectionEnd -= BoardPlaceSelection_OnBoardPlaceSelectionEnd;
        // UIActionPhase.OnAttackSelected += UIActionPhase_OnAttackSelected;
    }

    // private void UIActionPhase_OnAttackSelected(Card card, bool isPlayerTurn){
    //     Debug.Log("UIActionPhase_OnAttackSelected");
    //     if(isPlayerTurn){
    //         HighLightEnemyOcuppiedPlaces(card);
    //     }else{
    //         HighLightPlayerOcuppiedPlaces(card);
    //     }
    // }

    public void AttackSelected(Card card, bool isPlayerTurn){
        Debug.Log("AttackSelected");
        if(isPlayerTurn){
            HighLightEnemyOcuppiedPlaces(card);
        }else{
            HighLightPlayerOcuppiedPlaces(card);
        }
    }

    private void BoardPlaceSelection_OnBoardPlaceSelectionEnd(Card card, bool isPlayerTurn){
        if(isPlayerTurn){
            LightUpPlayerPlaces();
        }else{
            LightUpEnemyPlaces();
        }
    }

    private void BoardPlaceSelection_OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn){
        if(isPlayerTurn){
            HighLightPlayerPlaces(card);
        }else{
            HighLightEnemyPlaces(card);
        }
    }

    private void HighLightPlayerOcuppiedPlaces(Card card){
        Debug.Log("HighLightPlayerOcuppiedPlaces");
        List<BoardPlaceVisuals> list;

        if(card is MonsterCard){
            list = PlayerMonsterPlaces;
        }else{
            list = PlayerArcanePlaces;
        }

        foreach (BoardPlaceVisuals place in list){
            if(!place.IsFree){
                place.HighLight();
            }
        }
    }
    private void HighLightEnemyOcuppiedPlaces(Card card){
        Debug.Log("HighLightEnemyOcuppiedPlaces");
        List<BoardPlaceVisuals> list;

        if(card is MonsterCard){
            list = EnemyMonsterPlaces;
        }else{
            list = EnemyArcanePlaces;
        }

        foreach (BoardPlaceVisuals place in list){
            if(!place.IsFree){
                Debug.Log("!place.IsFree");
                place.HighLight();
            }else{
                Debug.Log("place.IsFree");
            }
        }
    }

    private void HighLightPlayerPlaces(Card card){
        if(card is MonsterCard){
            foreach (BoardPlaceVisuals place in PlayerMonsterPlaces){
                place.HighLight();
            }
        }else{
            foreach (BoardPlaceVisuals place in PlayerArcanePlaces){
                place.HighLight();
            }
        }
    }

    private void HighLightEnemyPlaces(Card card){
        if(card is MonsterCard){
            foreach (BoardPlaceVisuals place in EnemyMonsterPlaces){
                place.HighLight();
            }
        }else{
            foreach (BoardPlaceVisuals place in EnemyArcanePlaces){
                place.HighLight();
            }
        }
    }

    private void StartPhase_OnStartPhase(){
        LightUpPlayerPlaces();
        LightUpEnemyPlaces();
    }

    private void LightUpPlayerPlaces(){
        foreach (BoardPlaceVisuals place in PlayerMonsterPlaces){
            place.LightUp();
        }
        foreach (BoardPlaceVisuals place in PlayerArcanePlaces){
            place.LightUp();
        }
    }

    private void LightUpEnemyPlaces(){
        foreach (BoardPlaceVisuals place in EnemyMonsterPlaces){
            place.LightUp();
        }
        foreach (BoardPlaceVisuals place in EnemyArcanePlaces){
            place.LightUp();
        }
    }
}