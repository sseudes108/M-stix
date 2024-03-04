using UnityEngine;

public abstract class BoardCardPlacement : MonoBehaviour {
    [SerializeField] protected Collider _collider;

    protected void Awake() {
        _collider = GetComponent<Collider>();
    }
    
    private void OnMouseDown() {
        string cardPlacement;

        if (this is BoardCardMonsterPlace){
            cardPlacement = "Monster Place";
        }else{
            cardPlacement = "Arcane Place";
        }

        Debug.Log($"Click on {cardPlacement}");
    }
}