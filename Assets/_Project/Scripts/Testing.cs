using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour{
    
    [SerializeField] private List<MonsterCard> cards;

    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            Fusion.Instance.FusionCards(cards);
        }
    }
}
