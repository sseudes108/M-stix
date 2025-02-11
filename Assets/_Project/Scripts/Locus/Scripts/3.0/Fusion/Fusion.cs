using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class Fusion : MonoBehaviour {
        private FusionManager _fusionManager;

        private List<Card> _fusionLine;
        private Card _resultCard;
        
        private bool _isPlayerTurn;
        private bool _isBoardFusion;

        private void Awake() {
            _fusionManager = GetComponent<FusionManager>();
        }

        public void StartFusionRoutine(List<Card> selectedCards, bool isPlayerTurn){
            StartCoroutine(FusionRoutine(selectedCards, isPlayerTurn));
        }

        private IEnumerator FusionRoutine(List<Card> selectedCards, bool isPlayerTurn){        
            _isPlayerTurn = isPlayerTurn;
            _fusionLine = selectedCards;
            _resultCard = null;

            ResetCards(_fusionLine);

            if(_fusionLine.Count > 1){
                do{
                    
                    _fusionManager.MoveCardsToFusionPosition(_fusionLine, isPlayerTurn);       
        
                    yield return new WaitForSeconds(1f);

                    var card1 = _fusionLine[0];
                    var card2 = _fusionLine[1];
                    var cardType1 = card1.GetType();
                    var cardType2 = card2.GetType();

                    //Types Not Equals (arcane x monster / monster x arcane)
                    if(cardType1 != cardType2){
                        //Equip Fusion
                        RemoveCardsFromFusionLine(card1, card2);
                        //Time for the Equip fusion Coroutine finish
                        yield return new WaitForSeconds(2f);
                        yield break;
                    }

                    //Equal types
                    if(cardType1 == cardType2){
                        //Monster x Monster
                        yield return new WaitForSeconds(2f);
                        if(card1 is MonsterCard){
                            //Fusion Monster
                            _fusionManager.StartMonsterFusionRoutine(card1 as MonsterCard, card2 as MonsterCard);
                            yield return null;
                            RemoveCardsFromFusionLine(card1, card2);
                            //Time for the Monster fusion Coroutine finish
                            yield return new WaitForSeconds(2f);
                        }else{
                            //Arcane Fusion
                            yield return null;
                            RemoveCardsFromFusionLine(card1, card2);
                            yield return new WaitForSeconds(2f);
                        }
                    }
                    yield return new WaitForSeconds(1f);

                }while(_fusionLine.Count > 1);

            }else if(_fusionLine.Count == 1){

                _resultCard = _fusionLine[0];

                yield return null;
                _fusionManager.MoveCardToResultPosition(_resultCard, _isPlayerTurn);
            }

            if(!_isPlayerTurn){
                // _aIManager.Actor.SetFusionedCard(_resultCard);
                Debug.LogWarning("AI - Card Fusioned");
            }

            if(_resultCard is MonsterCard){
                (_resultCard as MonsterCard).SetCanAttack(false);
            }
        
            yield return new WaitForSeconds(1f);

            //Board Fusion
            if(_isBoardFusion){
                _resultCard.SetCardAsFusioned();
                _resultCard.ResetCardStats();
                _isBoardFusion = false;
            }

            // Open UI Select options
            _fusionManager.SetResultCard(_resultCard);

            _fusionLine.Clear();
            _fusionManager.FusionEnded();
        }

        private void RemoveCardsFromFusionLine(Card card1, Card card2){
            _fusionLine.Remove(card1);
            _fusionLine.Remove(card2);
        }

        private void AddCardToFusionLine(Card cardToAdd){
            _fusionLine.Insert(0, cardToAdd);
        }

        private void ResetCards(List<Card> selectedcards){
            foreach(var card in selectedcards){
                if(card.IsOnHand){
                    card.SetCardOnHand(false);
                    card.SetHandPositionFree();
                    card.ResetBorderColor();
                }
            }
        }

    #region Fusion Failed

        public void FusionFailed(Card card1, Card card2){
            StartCoroutine(FusionFailedRoutine(card1, card2));
        }

        private IEnumerator FusionFailedRoutine(Card card1, Card card2){
            yield return null;
            //Set Result of fusion Card
            _resultCard = null;
            _resultCard = card2;

            //Cards used in fusion
            var materials = new List<Card>() {card1, card2};

            //Move cards
            _fusionManager.MoveCardsToMergePosition(materials, _isPlayerTurn);

            // Camera Shake
            if(_isPlayerTurn){
                _fusionManager.ShakeCamera();
                // Debug.LogWarning("Implement Camera Shake");
            }

            yield return new WaitForSeconds(0.05f);

            //Dissolve the first card
            card1.DissolveCard(Color.red);

            yield return new WaitForSeconds(0.5f);

            //Destroy Card
            card1.DisableRenderer();
            yield return null;
            card1.DestroyCard();

            //Check if the line is 0
            if(_fusionLine.Count > 0){
                AddCardToFusionLine(card2);
            }else{
                // OnFusionRoutineFinished?.Invoke(card2, _isPlayerTurn);
                // BattleManager.Instance.FusionPositions.MoveCardToResultPosition(card2);
            }
        }

    #endregion
    
    #region Fusion Sucess
        public void FusionSucess(Card card1, Card card2, Card resultCard){
            StartCoroutine(FusionSucessRoutine(card1, card2, resultCard));
        }

        private IEnumerator FusionSucessRoutine(Card card1, Card card2, Card resultCard){
            //Set Result of fusion Card
            _resultCard = null;
            _resultCard = resultCard;

            //Cards used in fusion
            var materials = new List<Card>() {card1, card2};

            //Move cards
            _fusionManager.MoveCardsToMergePosition(materials, _isPlayerTurn);

            //Dissolve cards used
            yield return new WaitForSeconds(0.3f);
            foreach(var card in materials){
                card.DissolveCard(Color.green);
            }

            //Destroy Cards
            yield return new WaitForSeconds(0.7f);
            foreach(var card in materials){
                card.DestroyCard();
            }

            //Set Card Owner
            if(_isPlayerTurn){
                resultCard.SetPlayerCard();
            }

            //Move fusioned card to position
            _fusionManager.MoveCardToResultPosition(resultCard, _isPlayerTurn);

            //Check if the line is 0
            if(_fusionLine.Count > 0){
                AddCardToFusionLine(resultCard);
            }
        }

        #endregion

    }
}