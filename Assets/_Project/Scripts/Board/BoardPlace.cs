using System.Collections.Generic;
using UnityEngine;

public class BoardPlace : MonoBehaviour {
    [SerializeField] private List<Transform> _monsterPlaces;
    [SerializeField] private List<Transform> _arcanePlaces;

    protected List<BoardCardArcanePlace> _arcanesPlacement = new();
    protected List<BoardCardMonsterPlace> _monstersPlacement = new();

    private void Awake() {
        GetArcanePlacements();
        GetMonsterPlacements();
    }

    private void GetMonsterPlacements(){
        foreach(var place in _monsterPlaces){
            _monstersPlacement.Add(place.GetComponentInChildren<BoardCardMonsterPlace>());
        }
    }

    private void GetArcanePlacements(){
        foreach(var place in _arcanePlaces){
            _arcanesPlacement.Add(place.GetComponentInChildren<BoardCardArcanePlace>());
        }
    }

    public virtual List<BoardCardArcanePlace> ArcanePlacements => _arcanesPlacement;
    public virtual List<BoardCardMonsterPlace> MonsterPlacements => _monstersPlacement;
    public virtual List<Transform> MonsterPlaces => _monsterPlaces;
    public virtual List<Transform> ArcanePlaces => _arcanePlaces;
}