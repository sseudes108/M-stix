// using UnityEngine;
// using UnityEngine.Events;

// public class HandManagerSO : ScriptableObject {
//     [HideInInspector] public UnityEvent OnCardsDrew;

//     private void OnEnable() {
//         OnCardsDrew ??= new();
//     }

//     public void CardsDrew(){
//         OnCardsDrew?.Invoke();
//     }
// }