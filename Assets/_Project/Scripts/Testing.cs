using System.Collections.Generic;
using Mistix.FusionLogic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mistix{
    public class Testing : MonoBehaviour{
        [SerializeField] private Hand _hand;
        [SerializeField] private Card _card1, _card2;
        [SerializeField] private TMP_Text _turnDebugText;

        private void Awake() {
            _hand = GetComponent<Hand>();
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.D)){
                _hand.StartDrawCardRoutine();
            }

            if(Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if(Input.GetKeyDown(KeyCode.F)){
                // var selectedCards = new List<Card>{
                //     _card1,
                //     _card2
                // };
                var selectedCards = CardSelector.Instance.GetSelectedPlayerCardList();
                Fusion.Instance.StartFusionRoutine(selectedCards);
            }

            _turnDebugText.text = @$"Turn: {TurnSystem.GetTurnNumber().ToString()}
            IsPlayerTurn: {TurnSystem.IsPlayerTurn()}";
        }
    }
}
