using UnityEngine;

namespace Mistix{

    [RequireComponent(typeof(BoardPlaceVisual))]
    public class BoardPlace : MonoBehaviour {

        [field:SerializeField] public EBoardPlace Location { get; private set; }
        [field:SerializeField] public Collider[] Colliders { get; private set; }
        [field:SerializeField] public bool IsPlayerPlace { get; private set; }
        [field:SerializeField] public bool IsMonsterPlace { get; private set; }
        [field:SerializeField] public bool IsFree { get; private set; }

        private BoardPlaceVisual _visual;

        private void Awake() {
            Colliders = GetComponents<Collider>();
            _visual = GetComponent<BoardPlaceVisual>();
        }

        private void Start(){
            IsFree = true;
        }

        public void LightUp(Color color){
            _visual.LightUp(color);
        }

        public void LightOff(Color color){
            _visual.LightOff(color);
        }

        public void HighLight(){
            _visual.HighLight();
        }
    }
}