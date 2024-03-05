using UnityEngine;

public abstract class BoardCardPlacement : MonoBehaviour {
    [SerializeField] protected bool _isFree;
    protected Collider _collider;
    protected Renderer _renderer;

    protected void Awake() {
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
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

    public virtual Renderer Renderer => _renderer;
}