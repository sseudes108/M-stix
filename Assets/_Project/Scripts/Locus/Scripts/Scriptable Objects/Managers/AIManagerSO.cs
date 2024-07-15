using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AIManagerSO", menuName = "Mistix/Manager/AI", order = 0)]
public class AIManagerSO : ScriptableObject {
    [SerializeField] public BattleManagerSO BattleManager;
    [HideInInspector] public UnityEvent OnStateChange;

    public List<Card> CardsInHand = new(){};

    public List<BoardPlace> MonsterPlaces { get; private set; }
    public List<BoardPlace> ArcanePlaces { get; private set; }

    public AI AI { get; private set; }
    public Board Board { get; private set;}

    private void OnEnable() {
        OnStateChange ??= new();
    }

    public void OnDisable(){
        CardsInHand.Clear();
    }

    public void ChangeState(AI ai){
        SetAI(ai);
        OnStateChange?.Invoke();
    }

    public void SetAI(AI ai) { 
        AI = ai;
    }

    public void SetCardsInHand(List<Card> cardsInHand){
        CardsInHand = cardsInHand;
    }
}