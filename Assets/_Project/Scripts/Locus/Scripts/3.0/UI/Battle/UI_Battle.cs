using TMPro;
using UnityEngine;

namespace Mistix{   
    public class UI_Battle : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _turn;

        [Header("Player")]
        [SerializeField] private TextMeshProUGUI _playerLP;
        [SerializeField] private TextMeshProUGUI _playerDeck;

        [Header("Enemy")]
        [SerializeField] private TextMeshProUGUI _enemyLP;
        [SerializeField] private TextMeshProUGUI _enemyDeck;

        [Header("DEBUG")]
        [SerializeField] private TextMeshProUGUI _battlePhase;
        [SerializeField] private TextMeshProUGUI _aiState;

        public void UpdateTurn(int turn, bool playerTurn){
            var turnOwner = playerTurn ? "Player" : "AI";
            _turn.text = $"Turn: {turn} - {turnOwner}";
        }

        public void UpdateLifePoints(bool isPlayer, int lifePoints){
            if(isPlayer){
                _playerLP.text = $"LP: {lifePoints}";
            }else{
                _enemyLP.text = $"LP: {lifePoints}";
            }
        }

        public void UpdateDeckCount(bool isPlayer, int deckCount){
            if(isPlayer){
                _playerDeck.text = $"Deck: {deckCount}";
            }else{
                _enemyDeck.text = $"Deck: {deckCount}";
            }
        }

        public void UpdateDebugBattleState(string battlePhase){
            _battlePhase.text = $"Battle: {battlePhase}";
        }
    }
}