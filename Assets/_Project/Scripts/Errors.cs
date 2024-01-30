using UnityEngine;

namespace Mistix{
    public static class Errors{
        public static void InstanceError(MonoBehaviour sender){
            Debug.Log($"There's more than one Instance of {sender.name}");
        }
    }
}