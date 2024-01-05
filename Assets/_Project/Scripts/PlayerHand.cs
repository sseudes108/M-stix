using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour{
    [SerializeField] private Transform position1, position2, position3, position4, position5;
    [SerializeField] private List<Transform> _freePositionsInHand;
    [SerializeField] private Deck _deck;

    private void Start() {
        StartCoroutine(StartDrawRoutine());
    }

    public void DrawCards(){
        StartCoroutine(StartDrawRoutine());
    }

    private IEnumerator StartDrawRoutine(){
        Debug.Log("Draw Routine");
        CheckFreePositionInHand();
        yield return new WaitForSeconds(0.2f);

        for(int i = 0; i < _freePositionsInHand.Count; i++){
            yield return new WaitForSeconds(0.2f);
            int randomIndex = Random.Range(0, _deck.DeckInUse.Count);
            Card cardDrew = CardCreator.Instance.CreateCard(_deck.DeckInUse[randomIndex]);

            int pos = CheckThePosition(_freePositionsInHand);
            cardDrew.transform.SetPositionAndRotation(_freePositionsInHand[pos].position, _freePositionsInHand[pos].rotation);
            cardDrew.transform.SetParent(_freePositionsInHand[pos].transform);

            PlayerHandPositions handPosition = _freePositionsInHand[pos].GetComponent<PlayerHandPositions>();
            handPosition.SetPositionOccupied();
        }
    }

    private int CheckThePosition(List<Transform> freePositionsInHand){
        int pos = 0;
        foreach(Transform position in freePositionsInHand){
            PlayerHandPositions handPosition = position.GetComponent<PlayerHandPositions>();
            if(!handPosition.IsFree){
                pos++;
            }
        }
        return pos;
    }

    private void CheckFreePositionInHand(){
        _freePositionsInHand.Clear();

        PlayerHandPositions handPosition1 = position1.GetComponent<PlayerHandPositions>();
        if(handPosition1.IsFree && !_freePositionsInHand.Contains(position1)){
            _freePositionsInHand.Add(position1);
        }
        PlayerHandPositions handPosition2 = position2.GetComponent<PlayerHandPositions>();
        if(handPosition2.IsFree && !_freePositionsInHand.Contains(position2)){
            _freePositionsInHand.Add(position2);
        }
        PlayerHandPositions handPosition3 = position3.GetComponent<PlayerHandPositions>();
        if(handPosition3.IsFree && !_freePositionsInHand.Contains(position3)){
            _freePositionsInHand.Add(position3);
        }
        PlayerHandPositions handPosition4 = position4.GetComponent<PlayerHandPositions>();
        if(handPosition4.IsFree && !_freePositionsInHand.Contains(position4)){
            _freePositionsInHand.Add(position4);
        }
        PlayerHandPositions handPosition5 = position5.GetComponent<PlayerHandPositions>();
        if(handPosition5.IsFree && !_freePositionsInHand.Contains(position5)){
            _freePositionsInHand.Add(position5);
        }
    }
}
