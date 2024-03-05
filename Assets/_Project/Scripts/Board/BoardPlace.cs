using System.Collections.Generic;
using UnityEngine;

public class BoardPlace : MonoBehaviour {
    [SerializeField] protected List<Transform> _monsterPlaces;
    [SerializeField] protected List<Transform> _arcanePlaces;

    public virtual List<Transform> MonsterPlaces => _monsterPlaces;
    public virtual List<Transform> ArcanePlaces => _arcanePlaces;
}