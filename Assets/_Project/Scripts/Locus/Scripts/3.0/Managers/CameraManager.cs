using Cinemachine;
using UnityEngine;

namespace Mistix{
    public class CameraManager : MonoBehaviour {
        private CinemachineImpulseSource _impulse;
        private void Awake() { _impulse = GetComponent<CinemachineImpulseSource>(); }
        public void ShakeCamera(){
            _impulse.GenerateImpulse();
        }
    }
}