using UnityEngine;

namespace Mistix{
    public class HandPosition:MonoBehaviour{
        public bool _isFree = true;

        public void OcupyPlace(){
            _isFree = false;
        }
        public void FreePlace(){
            _isFree = true;
        }

        public bool IsFree() => _isFree;
    }
}