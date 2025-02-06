// using UnityEngine;
// using UnityEngine.Events;

// [CreateAssetMenu(fileName = "CameraManagerSO", menuName = "Mistix/Manager/Camera", order = 0)]
// public class CameraManagerSO : ScriptableObject {
//     [HideInInspector] public UnityEvent OnCamShake;

//     private void OnEnable() {
//         OnCamShake ??= new();
//     }

//     public void CamShake(){ OnCamShake?.Invoke(); }
// }