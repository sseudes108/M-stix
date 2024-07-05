using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraManager : MonoBehaviour {
    private CinemachineImpulseSource _impulse;
    private void Awake() {
        _impulse = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(){
        _impulse.GenerateImpulse();
    }
}