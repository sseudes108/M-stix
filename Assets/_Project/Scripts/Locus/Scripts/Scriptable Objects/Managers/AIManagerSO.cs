using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AIManagerSO", menuName = "Mistix/Manager/AI", order = 0)]
public class AIManagerSO : ScriptableObject {
    [HideInInspector] public UnityEvent OnStateChange;

    public AbstractState CurrentState => _ai.AICurrentState;
    public List<Card> CardsInHand = new();
    private AI _ai;
    public AI AI => _ai;

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

    public IEnumerator ChangeStateRoutine(float wait, AbstractState newState){
        yield return new WaitForSeconds(wait);
        _ai.ChangeState(newState);
        yield return null;
    }

    public void SetAI(AI ai) { 
        _ai = ai;
    }

    public void SetCardsInHand(List<Card> cardsInHand){
        CardsInHand = cardsInHand;
    }
}