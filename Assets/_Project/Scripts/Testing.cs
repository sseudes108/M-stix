using System.Collections.Generic;
using Mistix.FusionLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mistix{
    public class Testing : MonoBehaviour{
        [SerializeField] private Hand _hand;
        [SerializeField] private Card _card1, _card2;

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
                var selectedCards = new List<Card>{
                    _card1,
                    _card2
                };
                Fusion.Instance.StartFusionRoutine(selectedCards);
            }
        }
    }
}
