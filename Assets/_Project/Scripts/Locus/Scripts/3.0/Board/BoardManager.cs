using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    
    public class BoardManager : MonoBehaviour {
        public static BoardManager Instance { get; private set;}

        [SerializeField] private BattleManager _battleManager;
        private BoardPlaceVisualController _boardPlaceVisualController;

        private Color PlayerDefaultColor;
        private Color EnemyDefaultColor;

        [SerializeField] private List<BoardPlace> _playerMonsterPlaces;
        [SerializeField] private List<BoardPlace> _playerArcanePlaces;
        [SerializeField] private List<BoardPlace> _enemyMonsterPlaces;
        [SerializeField] private List<BoardPlace> _enemyArcanePlaces;

        public Quaternion PlayerMonsterFaceDownAtkRotation {get; private set;} = Quaternion.Euler(-90, -90, -90);
        public Quaternion PlayerMonsterFaceDownDefRotation {get; private set;} = Quaternion.Euler(-90, -180, -90);
        public Quaternion PlayerMonsterFaceUpDefRotation {get; private set;} = Quaternion.Euler(90, 90, 0);
        
        public Quaternion EnemyMonsterFaceDownAtkRotation {get; private set;} = Quaternion.Euler(-90, -90, 90);
        public Quaternion EnemyMonsterFaceDownDefRotation {get; private set;} = Quaternion.Euler(-90, -180, 90);
        public Quaternion EnemyMonsterFaceUpDefRotation {get; private set;} = Quaternion.Euler(90, 90, 180);

        private bool _boardPlaceSelected = false;

        private void Awake() {
            if(Instance == null){
                Instance = this;
            }else{
                Debug.LogError("More Than One Instance of BoardManager");
            }
            
            _boardPlaceVisualController = new(); 
        }

        public void LightUpAllPlaces(){
            _boardPlaceVisualController.LightUpPlaces(_playerMonsterPlaces, PlayerDefaultColor);
            _boardPlaceVisualController.LightUpPlaces(_playerArcanePlaces, PlayerDefaultColor);

            _boardPlaceVisualController.LightUpPlaces(_enemyMonsterPlaces, EnemyDefaultColor);
            _boardPlaceVisualController.LightUpPlaces(_enemyArcanePlaces, EnemyDefaultColor);
        }

        public void LightOffAllPlaces() { 
            _boardPlaceVisualController.LightOffPlaces(_playerMonsterPlaces, PlayerDefaultColor);
            _boardPlaceVisualController.LightOffPlaces(_playerArcanePlaces, PlayerDefaultColor);

            _boardPlaceVisualController.LightOffPlaces(_enemyMonsterPlaces, EnemyDefaultColor);
            _boardPlaceVisualController.LightOffPlaces(_enemyArcanePlaces, EnemyDefaultColor);
        }

        public void SetPlaceColors(Vector3 playerBoardColor, Vector3 enemyBoardColor){
            PlayerDefaultColor = new Color(playerBoardColor.x, playerBoardColor.y, playerBoardColor.z);
            EnemyDefaultColor = new Color(enemyBoardColor.x, enemyBoardColor.y, enemyBoardColor.z);
        }

        public void HighLightFreePlaces(){
            if(_battleManager.GetFusionResultCard() is MonsterCard){
                HighlightFreeMonsterPlaces();
            }else{
                HighlightFreeArcanePlaces();
            }
        }

        private void HighlightFreeMonsterPlaces(){
            if(_battleManager.IsPlayerTurn()){
                foreach(var place in _playerMonsterPlaces){
                    if(place.IsFree){
                        _boardPlaceVisualController.HighlightPlace(place);
                    }
                }
            }else{
                foreach(var place in _enemyMonsterPlaces){
                    if(place.IsFree){
                        _boardPlaceVisualController.HighlightPlace(place);
                    }
                }
            }
        }

        private void HighlightFreeArcanePlaces(){}

        public bool IsBoardPlaceSelectionPhase(){ return _battleManager.IsBoardPlaceSelectionPhase(); }

        public bool IsPlayerTurn(){ return _battleManager.IsPlayerTurn(); }
        public Card GetResultCard(){ return _battleManager.GetFusionResultCard(); }

        public void ResetBoardPlaceSelected(){ _boardPlaceSelected = false; }
        public void BoardPlaceSelected(){ _boardPlaceSelected = true; }
        public bool IsBoardPlaceSelected(){ return _boardPlaceSelected; }

        public bool IsActionPhase(){ return _battleManager.IsActionPhase(); }

        public void ShowOptions(Card cardInPlace, BoardPlace place){ _battleManager.ShowOptions(cardInPlace, place); }
        public void HideOptions(){ _battleManager.HideOptions(); }

        public bool PlayerHasArcaneOnField(){
            bool arcaneOnField = false;
            foreach(var place in _playerArcanePlaces){
                if(place.IsFree){
                    continue;
                }else{
                    return true;
                }
            }
            return arcaneOnField;
        }

        public bool EnemyHasArcaneOnField(){
            bool arcaneOnField = false;
            foreach(var place in _enemyArcanePlaces){
                if(place.IsFree){
                    continue;
                }else{
                    return true;
                }
            }
            return arcaneOnField;
        }

        /// <summary>
        /// Return Monster and Arcane Places
        /// </summary>
        /// <returns></returns>
        public (List<BoardPlace>,List<BoardPlace>) GetAIPlaces(){
            return (_enemyMonsterPlaces, _enemyArcanePlaces);
        }
    }
}