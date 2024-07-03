// using System;
// using Cinemachine;
// using UnityEngine;

// public class CameraManager : MonoBehaviour {
//     [SerializeField] private CinemachineImpulseSource _impulseSource;
//     [SerializeField] private Transform _camera;

//     private void OnEnable() {
//         Fusion.OnFusionFailed += Fusion_OnFusionFailed;
//     }

//     private void OnDisable() {
//         Fusion.OnFusionFailed -= Fusion_OnFusionFailed;
//     }

//     private void Awake() {
//         _impulseSource = GetComponent<CinemachineImpulseSource>();
//     }

//     private void FusionFailed(){
//         _impulseSource.GenerateImpulseWithForce(0.2f);
//     }

//     private void Fusion_OnFusionFailed(){
//         FusionFailed();
//     }
// }