using System.Collections.Generic;
using UnityEngine;

public class BoardCardMonsterPlace : BoardCardPlacement {
    [SerializeField] private Renderer[] _renderers;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private void Awake() {
        base.Awake();
        _renderers = GetComponentsInChildren<Renderer>();
    }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

}