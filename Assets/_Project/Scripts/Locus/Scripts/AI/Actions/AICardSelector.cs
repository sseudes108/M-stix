using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector : AIAction{
    public AICardSelector(AIActorSO actor){
        _actor = actor;
    }
    
    private List<Card> _selectedList = new();
    public List<Card> SelectedList => _selectedList;

    public IEnumerator SelectCardRoutine(List<Card> cardsInHand, CardsOnField cardsOnField){
        _selectedList.Clear();
        _actor.FieldChecker.OrganizeCardsOnHand(cardsInHand);
        _actor.FieldChecker.OrganizeAIMonsterCardsOnField(cardsOnField.MonstersOnAIField);

        if(_actor.MakeABoardFusion){
            AddToSelectedList(_actor.AIManager.GetFusionedCard());
            AddToSelectedList(_actor.CardOnBoardToFusion);

            _actor.ResetBoardFusion();
        }else{
            if(cardsOnField.MonstersOnAIField.Count == 0){
                StrongestFusionFromHand();
            }else{
                StrongestFusionInBoard(cardsOnField.MonstersOnAIField);
            }
        }

        yield return new WaitForSeconds(Random.Range(1.5f, 3f));
        _actor.CardSelectionFinished();
        yield return null;
    }

    private void SelectRandomCard(List<Card> cardsInHand){
        var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
        AddToSelectedList(randomCard);
    }

    private void AddToSelectedList(Card card){
        _selectedList.Add(card);
    }

    private void StrongestFusionFromHand(){
        if(CanMakeALvl3FromHand()){
            TryMakeALvl3FromHand();
            return;
        }

        AddToSelectedList(_actor.FieldChecker.Lvl2OnHand[0]);
    }
    
    /// <summary>
    /// cardToFusion is the card on the field the will be used after the fusion from hand
    /// </summary>
    private void BoardFusion(Card cardToFusion){
        _actor.BoardManager.BoardFusion();
        _actor.MakeABoardFusion = true;
        _actor.CardOnBoardToFusion = cardToFusion;
    }

    private void StrongestFusionInBoard(List<MonsterCard> monstersOnAIField){
        if(CanMakeALvl8()){
            TryMakeALvl8();
            return;
        }

        if(CanMakeALvl7()){
            TryMakeALvl7();
            return;
        }

        if(CanMakeALvl6()){
            TryMakeALvl6();
            return;
        }

        if(CanMakeALvl5()){
            TryMakeALvl5();
            return;
        }else{
            if(CanMakeALvl5FromHand()){
                TryMakeALvl5FromHand();
                return;
            }
        }

        if(CanMakeALvl4()){
            TryMakeALvl4();
            return;
        }else{
            if(CanMakeALvl4FromHand()){
                TryMakeALvl4FromHand();
                return;
            }
        }

        if(CanMakeALvl3()){
            TryMakeALvl3();
            return;
        }else{
            if(CanMakeALvl3FromHand()){
                TryMakeALvl3FromHand();
                return;
            }
        }

        /*
            Organizar as cartas da AI em campo. - OK
            Verificar qual o lvl mais alto em campo. - OK
            Verificar se é possivel, da mão, fazer outro monstro com o msm lvl
            Se for possivel, fusionar o monstro da mão e depois, fusionar com o monstro em campo
            Se não for possivel, fusionar o monstro mais forte possivel da mão.
        */
    }

#region Level 8
    private bool CanMakeALvl8(){
        // Verificar se há nivel 7 em campo;
        if(_actor.FieldChecker.Lvl7OnAIField.Count != 0){
            // Verificar se há nivel 6 em campo;
            if(_actor.FieldChecker.Lvl6OnAIField.Count != 0){

                // Verificar se há nivel 5 em campo;
                if(_actor.FieldChecker.Lvl5OnAIField.Count != 0){
                    //tem um nv7, um nv6 e um nv5 em campo;

                    if(_actor.FieldChecker.Lvl4OnAIField.Count != 0){
                        //Tem um nv4 em campo

                        //Tem um nv4 na mão ou pode ser fundido da mão?
                        if(CanMakeALvl4FromHand()){
                            return true;
                        }else{
                            return false;
                        }
                    }

                }else{//nv5
                    //Existe um 7 e um 6 em campo, porém, não há lvl 5;
                    return false;
                }

            }else{//nv6
                //Existe um 7 em campo. porém, nao há lvl 6;
                return false;
            }

        }else{//nv7
            //Não há lvl 7 em campo;
            return false;
        }

        return false;
    }

    private void TryMakeALvl8(){

    }
#endregion

#region Level 7
    private bool CanMakeALvl7(){
        // Verificar se há nivel 6 em campo;
        if(_actor.FieldChecker.Lvl6OnAIField.Count != 0){

            // Verificar se há nivel 5 em campo;
            if(_actor.FieldChecker.Lvl5OnAIField.Count != 0){
                //nv6 e um nv5 em campo;

                if(_actor.FieldChecker.Lvl4OnAIField.Count != 0){
                    //Tem um nv4 em campo

                    //Tem um nv4 na mão ou pode ser fundido da mão?
                    if(CanMakeALvl4FromHand()){
                        return true;
                    }else{
                        return false;
                    }
                }

            }else{//nv5
                //Existe um nv6 em campo, porém, não há lvl nv5 em campo
                return false;
            }

        }else{//nv6
            //Nao há lvl nv6 em campo
            return false;
        }

        return false;
    }

    private void TryMakeALvl7(){

    }
#endregion

#region Level 6
    private bool CanMakeALvl6(){
        // Verificar se há nivel 5 em campo;
        if(_actor.FieldChecker.Lvl5OnAIField.Count != 0){
            //nv6 e um nv5 em campo;

            if(_actor.FieldChecker.Lvl4OnAIField.Count != 0){
                //Tem um nv4 em campo

                //Tem um nv4 na mão ou pode ser fundido da mão?
                if(CanMakeALvl4FromHand()){
                    return true;
                }else{
                    return false;
                }
            }

        }else{//nv5
            //Não há lvl nv5 em campo
            return false;
        }
        
        return false;
    }

    private void TryMakeALvl6(){

    }
#endregion

#region Level 5
    private bool CanMakeALvl5(){
        //Há um nv4 no campo
        if(_actor.FieldChecker.Lvl4OnAIField.Count > 0){
            // é possivel fazer um nv4 da mão
            if(CanMakeALvl4FromHand()){
                return true;
            }
        }else{
            return false;
        }

        return false;
    }
    
    private bool CanMakeALvl5FromHand(){
        //Mais de um nv4 na mão
        if(_actor.FieldChecker.Lvl4OnHand.Count > 1){
            return true;
        }

        //Mais de um nv3 e um nv4 na mão
        if(_actor.FieldChecker.Lvl3OnAIField.Count > 1 && _actor.FieldChecker.Lvl4OnHand.Count > 0){
            return true;
        }

        return false;
    }

    private void TryMakeALvl5(){

    }
    private void TryMakeALvl5FromHand(){

    }
#endregion

#region Level 4

    private bool CanMakeALvl4(){
        if(_actor.FieldChecker.Lvl3OnAIField.Count > 0){
            if(CanMakeALvl3FromHand()){
                return true;
            }
        }
        return false;
    }

    private bool CanMakeALvl4FromHand(){
        //Mais um 0 nv4 na mão.
        if(_actor.FieldChecker.Lvl4OnHand.Count != 0){
            return true;
        }

        //Mais de um nv3 na mão
        if(_actor.FieldChecker.Lvl3OnHand.Count > 1){
            return true;
        }

        //Mais de um nv2 na mão e um nv3 na mão (maior que 0, porém, não maior que 1)
        if(_actor.FieldChecker.Lvl2OnHand.Count > 1 && _actor.FieldChecker.Lvl3OnHand.Count > 0){
            return true;
        }

        return false;
    }

    private void TryMakeALvl4(){
        if(_actor.FieldChecker.Lvl3OnAIField.Count > 0){
            if(CanMakeALvl3FromHand()){
                TryMakeALvl3FromHand();
                BoardFusion(_actor.FieldChecker.Lvl3OnAIField[0]);
            }
        }
    }

    private void TryMakeALvl4FromHand(){

    }
#endregion

#region Level 3
    private bool CanMakeALvl3(){
        if(_actor.FieldChecker.Lvl2OnAIField.Count > 0){
            if(_actor.FieldChecker.Lvl2OnHand.Count > 0){
                return true;
            }else{
                return false;
            }
        }else{
            return false;
        }
    }

    private bool CanMakeALvl3FromHand(){
        //Mais um 0 nv3 na mão
        if(_actor.FieldChecker.Lvl3OnHand.Count != 0){
            return true;
        }

        //Mais de um nv2 na mão
        if(_actor.FieldChecker.Lvl2OnHand.Count > 1){
            return true;
        }

        return false;
    }
    private void TryMakeALvl3(){

    }

    private void TryMakeALvl3FromHand(){
        if(_actor.FieldChecker.Lvl3OnHand.Count != 0){
            AddToSelectedList(_actor.FieldChecker.Lvl3OnHand[0]);
            return;
        }

        if(_actor.FieldChecker.Lvl2OnHand.Count > 1){
            AddToSelectedList(_actor.FieldChecker.Lvl2OnHand[0]);
            AddToSelectedList(_actor.FieldChecker.Lvl2OnHand[1]);
            return;
        }
    }
#endregion

}