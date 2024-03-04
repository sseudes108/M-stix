using UnityEngine;

public class BoardCardArcanePlace : BoardCardPlacement {
    [SerializeField] private Renderer _renderer;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private void Awake() {
        base.Awake();
        _renderer = GetComponentInChildren<Renderer>();
    }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

}