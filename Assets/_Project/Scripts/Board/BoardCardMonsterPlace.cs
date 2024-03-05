using UnityEngine;

public class BoardCardMonsterPlace : BoardCardPlacement {
    [SerializeField] private Renderer[] _renderers;
    public Renderer[] Renderers => _renderers;
}