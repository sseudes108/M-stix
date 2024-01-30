using System.Collections.Generic;
using Mistix;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour{
    [SerializeField] Hand _hand;

    [SerializeField] Card _card1, _card2;

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
            var selectedCards = new List<Card>();
            selectedCards.Add(_card1);
            selectedCards.Add(_card2);
            Fusion.Instance.StartFusion(selectedCards);
        }
    }
}
