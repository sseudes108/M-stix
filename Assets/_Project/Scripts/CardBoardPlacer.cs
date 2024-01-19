using System.Collections.Generic;
using UnityEngine;

public class CardBoardPlacer : MonoBehaviour {
    [SerializeField] private List<Transform> _playerMonsterCards;
    [SerializeField] private List<Transform> _playerArcaneCards;
    [SerializeField] private List<Transform> _CPUMonsterCards;
    [SerializeField] private List<Transform> _CPUArcaneCards;

    public void PlacePlayerMonsterCard(Card card){
        card.transform.position = _playerMonsterCards[0].position;
        card.transform.SetParent(_playerMonsterCards[0]);
        card.transform.rotation = Quaternion.Euler(90, 0, 0);
        card.transform.localScale = new Vector3(0.2f, 0.13f, 0.14f);
        _playerMonsterCards.Remove(_playerMonsterCards[0]);
    }
    public void PlacePlayerArcaneCard(Card card){
        card.transform.position = _playerArcaneCards[0].position;
        card.transform.SetParent(_playerArcaneCards[0]);
        card.transform.rotation = Quaternion.Euler(90, 0, 0);
        card.transform.localScale = new Vector3(0.2f, 0.13f, 0.14f);
        _playerArcaneCards.Remove(_playerArcaneCards[0]);
    }
    public void PlaceCPUMonsterCard(Card card){
        card.transform.position = _playerMonsterCards[0].position;
        _playerMonsterCards.Remove(_playerMonsterCards[0]);
    }
    public void PlaceCPUArcaneCard(Card card){
        card.transform.position = _playerArcaneCards[0].position;
        _playerArcaneCards.Remove(_playerArcaneCards[0]);
    }
}