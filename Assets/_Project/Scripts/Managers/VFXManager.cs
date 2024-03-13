using UnityEngine;

public class VFXManager : MonoBehaviour{
    [SerializeField] private GameObject _lowDamageParticle, _mediuDemageParticle, _highDamageParticle;

    public void VFXLowDamageParticle(Transform position){
        Instantiate(_lowDamageParticle, position);
    }
}
