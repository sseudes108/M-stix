namespace Mistix{
    using UnityEngine;

    [RequireComponent(typeof(BoardPlaceVisual))]
    public class BoardPlace : MonoBehaviour {

        [field:SerializeField] public EBoardPlace Location { get; private set; }
        [field:SerializeField] public Collider[] Colliders { get; private set; }
        [field:SerializeField] public bool IsPlayerPlace { get; private set; }
        [field:SerializeField] public bool IsMonsterPlace { get; private set; }
        [field:SerializeField] public bool IsFree { get; private set; }

        public BoardPlaceVisual Visual;

        private void Awake() {
            Colliders = GetComponents<Collider>();
            Visual = GetComponent<BoardPlaceVisual>();
        }

        private void Start(){
            IsFree = true;
        }
    }
}