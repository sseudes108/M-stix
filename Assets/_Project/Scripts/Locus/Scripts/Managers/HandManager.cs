using UnityEngine;

public class HandManager : MonoBehaviour {
    public PlayerHand Player { get; private set; }
    public EnemyHand Enemy { get; private set; }

    private void Awake() {
        Player = transform.Find("Player").GetComponent<PlayerHand>();
        Enemy = transform.Find("Enemy").GetComponent<EnemyHand>();
    }
}