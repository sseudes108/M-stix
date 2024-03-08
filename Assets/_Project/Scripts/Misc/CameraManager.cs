using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    [SerializeField] private Transform _camera;
    
    private void Awake() {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void FusionFailed(){
        _impulseSource.GenerateImpulseWithForce(0.2f);
    }
}