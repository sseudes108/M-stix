using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifShader : MonoBehaviour {
    [SerializeField] private Renderer _renderer;

    [SerializeField] private List<Texture2D> _gifFrames;
    [SerializeField] private Texture2D _frame;
    [SerializeField] private float _frameDelay;
    private int index = 0;

    private void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void Update() {
        if(index == 0){
            MakeAnimation(_gifFrames);
        }
        SetChangesToMaterial();
    }

    private void MakeAnimation(List<Texture2D> _gifFrames){
        StartCoroutine(MakeAnimationRoutine(_gifFrames));
    }

    private IEnumerator MakeAnimationRoutine(List<Texture2D> _gifFrames){
        do{
            _frame = _gifFrames[index];
            index++;
            yield return new WaitForSeconds(_frameDelay);
        }while(index < _gifFrames.Count);
        index = 0;
    }

    private void SetChangesToMaterial(){
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Gif", _frame);
        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat};
    }
}