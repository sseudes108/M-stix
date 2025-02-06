// using Cinemachine;
// using UnityEngine;

// [RequireComponent(typeof(CinemachineImpulseSource))]
// public class GameCamera : MonoBehaviour {
//     private CinemachineImpulseSource _impulse;
//     [SerializeField] private CameraManagerSO _cameraManager;

//     private void OnEnable() {
//         _cameraManager.OnCamShake.AddListener(CameraManager_OnCamShake);
//     }

//     private void OnDisable() {
//         _cameraManager.OnCamShake.RemoveListener(CameraManager_OnCamShake);
//     }

//     private void Awake() { _impulse = GetComponent<CinemachineImpulseSource>(); }

//     private void CameraManager_OnCamShake(){
//         _impulse.GenerateImpulse();
//     }
// }