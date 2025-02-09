using UnityEngine;

//Class used for deactivate the cards used to hold the positions for card used on battle (Handpositions, fusion positions, etc)
namespace Mistix{
    public class CardHolder : MonoBehaviour {
        private void Start() {
            gameObject.SetActive(false);
        }
    }
}