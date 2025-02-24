using System.Collections;
using UnityEngine;

namespace Mistix{
    
    public class BoardPlaceVisual : MonoBehaviour {
        private BoardPlace _place;
        private Renderer[] _renderers;
        private float _intensityFactor;
        public bool IsFree => _place.IsFree;

        private void Awake() {
            _renderers = GetComponentsInChildren<Renderer>();
            _place = GetComponent<BoardPlace>();

        }

        public void LightOff(Color color){
            StartCoroutine(SetColorRoutine(color, 0.01f, true));
        }

        public void LightUp(Color color){
            StartCoroutine(SetColorRoutine(color, 0.2f, false));
        }
        
        public void HighLight(){
            StartCoroutine(SetColorRoutine(new Color(216, 216, 27), 0.1f, false));
        }

        public void UnHighLight(Color color){
            StartCoroutine(SetColorRoutine(color, 0.2f, false));
        }

        public IEnumerator SetColorRoutine(Color newColor, float intensity, bool imediate){
            Color adjustedColor = new(
                newColor.r * intensity, 
                newColor.g * intensity, 
                newColor.b * intensity,
                newColor.a
            );
            
            if(imediate){
                foreach(var renderer in _renderers){
                    var newBorderMaterial = new Material(renderer.sharedMaterials[1]);
                    ChangeMat(renderer, newBorderMaterial, adjustedColor, intensity);
                }
            }else{
                _intensityFactor = 0;
                var intensityTarget = intensity*2.5f;
                foreach(var renderer in _renderers){
                    StartCoroutine(ChangeBoardColorRoutine(renderer, adjustedColor, _intensityFactor, intensityTarget));
                }
                yield return null;
            }
        }

        private void ChangeMat(Renderer renderer, Material newBorderMaterial, Color adjustedColor, float intensity){
            newBorderMaterial.SetColor("_BorderColor", adjustedColor);
            newBorderMaterial.SetFloat("_Intensity", intensity);
            renderer.materials = new[] { renderer.sharedMaterials[0], newBorderMaterial, renderer.sharedMaterials[2] };
        }

        private IEnumerator ChangeBoardColorRoutine(Renderer renderer, Color adjustedColor, float intensityFactor, float intensityTarget){
            var newBorderMaterial = new Material(renderer.sharedMaterials[1]);
            do{
                ChangeMat(renderer, newBorderMaterial, adjustedColor, intensityFactor);
                intensityFactor += Time.deltaTime;
                yield return null;
            }while(intensityFactor < intensityTarget);
            yield return null;
        }
    }
}