// using UnityEngine;
// using UnityEngine.Events;

// [CreateAssetMenu(fileName = "HandEventsHandlerSO", menuName = "Mistix/Events/Hand", order = 0)]
// public class HandEventHandlerSO : ScriptableObject {
//     public UnityEvent OnCardsDrew;

//     private void OnEnable() {
//         OnCardsDrew ??= new UnityEvent();
//     }

//     public void CardsDrew() { OnCardsDrew?.Invoke(); }
// }