using Unity.Mathematics;
using UnityEngine;

public class VFXManager : MonoBehaviour{
    [SerializeField] private GameObject _lowDamageParticle, _mediumDemageParticle, _highDamageParticle;

    public void VFXLowDamageParticle(Transform transform){
        Instantiate(_lowDamageParticle, transform.position, quaternion.identity);
    }
}
