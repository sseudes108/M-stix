using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {
    [SerializeField] private Card _resultCard;
    [SerializeField] private List<Card> _fusionLine;

    public void StartFusionRoutine(List<Card> selectedCards){
        StartCoroutine(FusionRoutine(selectedCards));
    }
    private IEnumerator FusionRoutine(List<Card> selectedCards){
        float waitTime = 2f;

        //Reset Border card Colors
        BattleManager.Instance.CardVisuals.ResetBorderColors(selectedCards);

        _fusionLine = selectedCards;
        if(selectedCards.Count > 1){
            do{
                _resultCard = null;
                
                //Move cards to fusion line positions
                BattleManager.Instance.FusionPositions.MoveCardToPosition(selectedCards);

                yield return new WaitForSeconds(waitTime/3);

                var card1 = _fusionLine[0];
                var card2 = _fusionLine[1];

                //Types Not Equals (arcane x monster / monster x arcane)
                if(card1.GetCardType() != card2.GetCardType()){
                    //FusionEquip
                    EquipeFusion(card1, card2);                
                    RemoveCardsFromFusionLine(card1, card2);

                    //Time for the Equip fusion Coroutine finish
                    yield return new WaitForSeconds(waitTime);
                }

                //Type Equals (monster x monster / arcane x arcane)
                if(card1.GetCardType() == card2.GetCardType()){
                    yield return new WaitForSeconds(waitTime);

                    if(card1.GetCardType() == ECardType.Monster){
                        //FusionMonster
                        MonsterFusion(card1 as CardMonster, card2 as CardMonster);
                        RemoveCardsFromFusionLine(card1, card2);

                        //Time for the Monster fusion Coroutine finish
                        yield return new WaitForSeconds(waitTime);

                    }else if(card1.GetCardType() == ECardType.Arcane){
                        //FusionArcane
                        ArcaneFusion(card1 as CardArcane, card2 as CardArcane);
                        RemoveCardsFromFusionLine(card1, card2);

                        //Time for the Arcane fusion Coroutine finish
                        yield return new WaitForSeconds(waitTime);
                    }
                }
            }while(selectedCards.Count > 0);
        }else if(selectedCards.Count == 1){
            //Caso tenha apenas uma carta na lista o resultado será ela
            _resultCard = selectedCards[0];
            _resultCard.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosistion());
        }
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.SelectionsPhase);
    }

    private void MonsterFusion(CardMonster monster1, CardMonster monster2){
        BattleManager.Instance.FusionMonster.MonsterFusion(monster1, monster2);
    }
    private void ArcaneFusion(CardArcane arcane1, CardArcane arcane2){
        BattleManager.Instance.FusionArcane.ArcaneFusion(arcane1, arcane2);
    }
    private void EquipeFusion(Card card1, Card card2){
        BattleManager.Instance.FusionEquip.EquipFusion(card1, card2);
    }

    //Fusion Line
    public void RemoveCardsFromFusionLine(Card card1, Card card2){
        _fusionLine.Remove(card1);
        _fusionLine.Remove(card2);
    }
    public void AddCardToFusionLine(Card cardToAdd){
        _fusionLine.Insert(0, cardToAdd);
    }
    public int GetCardsInFusionLine() => _fusionLine.Count;

    //Fusion Result
    public Card GetResultCard() => _resultCard;

    //Fusion process//
    //Fusion Failed
    public void FusionFailed(Card card1, Card card2){
       StartCoroutine(FusionFailedRoutine(card1, card2));
    }
    private IEnumerator FusionFailedRoutine(Card card1, Card card2){
        //Set Result of fusion Card
        _resultCard = card2;
        _resultCard.SetFusionedCard();

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        BattleManager.Instance.FusionPositions.MergeCards(materials);


        //Camera eff
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.CameraManager.FusionFailed();
        }
        yield return new WaitForSeconds(0.1f);

        //Dissolve the first card
        BattleManager.Instance.CardVisuals.DissolveCard(card1, Color.red);
        yield return new WaitForSeconds(0.5f);

        //Destroy Card
        card1.DisableModelVisual();
        card1.DestroyCard();

        //Check if the line is 0
        if(GetCardsInFusionLine() > 0){
            AddCardToFusionLine(card2);
        }else{
            BattleManager.Instance.FusionPositions.MoveCardToResultPosition(card2);

        }
    }

    //Monster Fusion Sucess
    public void FusionSucess(Card card1, Card card2, Card resultCard){
        StartCoroutine(FusionSucessRoutine(card1, card2, resultCard));
    }
    private IEnumerator FusionSucessRoutine(Card card1, Card card2, Card resultCard){
        //Set Result of fusion Card
        _resultCard = resultCard;

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        BattleManager.Instance.FusionPositions.MergeCards(materials);

        //Dissolve cards used
        yield return new WaitForSeconds(0.3f);
        BattleManager.Instance.CardVisuals.DissolveCard(materials, Color.green);

        //Destroy Cards
        yield return new WaitForSeconds(0.7f);
        card1.DestroyCard();
        card2.DestroyCard();

        //Set Card Owner
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            resultCard.SetPlayerCard();

        }

        //Move fusioned card to position
        resultCard.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosistion());

        //Check if the line is 0
        if(GetCardsInFusionLine() > 0){
            AddCardToFusionLine(resultCard);
        }
    }

    //Equip Fusion Sucess
    public void FusionSucess(CardArcane card1, CardMonster card2){
        StartCoroutine(FusionSucessRoutine(card1, card2));
    }
    private IEnumerator FusionSucessRoutine(CardArcane arcane, CardMonster monster){
        //Set Result of fusion Card
        _resultCard = monster;

        //Cards used in fusion
        List<Card> materials = new(){arcane, monster};

        //Move cards
        BattleManager.Instance.FusionPositions.MergeCards(materials);

        //Dissolve arcane card used
        yield return new WaitForSeconds(0.3f);
        BattleManager.Instance.CardVisuals.DissolveCard(arcane, Color.green);

        //Destroy Cards
        yield return new WaitForSeconds(0.7f);
        arcane.DestroyCard();

        //Set Card Owner
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            monster.SetPlayerCard();
        }

        //Move fusioned card to position
        monster.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosistion());

        //Check if the line is 0
        if(GetCardsInFusionLine() > 0){
            AddCardToFusionLine(monster);
        }
    }
}