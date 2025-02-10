using UnityEngine;

namespace Mistix{
    public class UI_CardHolder : MonoBehaviour {
        private Renderer _renderer;
        private CardMovement _cardMovement;
        private Vector3 _startPosition;
        [SerializeField] private Transform _offScreenPosition;
        private float _moveSpeed = 5f;

        private void Awake() {
            _renderer = transform.Find("Card").GetComponentInChildren<Renderer>();
            _cardMovement = transform.Find("Card").GetComponentInChildren<CardMovement>();
        }

        private void Start() { _startPosition = _renderer.transform.position; }

        public void UpdateIllustration(Texture2D illustration){
            var faceMat = new Material(_renderer.sharedMaterials[1]);
            faceMat.SetTexture("_Ilustration", illustration);

            _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
        }

        public void MoveOffScren(){
            _cardMovement.AllowMovement(true);
            _cardMovement.SetTargetPosition(_offScreenPosition.position, _moveSpeed);
        }

        public void MoveOnScren(){
            _cardMovement.AllowMovement(true);
            _cardMovement.SetTargetPosition(_startPosition, _moveSpeed);
        }
    }
}