using UnityEngine;
namespace Mistix{
    public class CardStatsSelecion : MonoBehaviour {
        private CardManager _cardManager;

        private void Awake() {
            _cardManager = GetComponent<CardManager>();
        }

        public void Option1_Clicked(Card card){
            if(card is MonsterCard){
                var monster = card as MonsterCard;
                if(monster.FusionedCard){
                    // fusioned Card
                    if(!monster.AnimaSelected){ // Anima not selected
                        monster.SelectAnima(1);
                        _cardManager.SelectAnother(monster);
                        return;                    
                    }

                    if(!monster.ModeSelected){ //Anima selected and Mode not selected
                        monster.SelectMode();
                        _cardManager.StatSelectionEnd();
                        return;
                    }

                    monster.SelectFace(); //Always face up
            
                }else{
                    if(!monster.AnimaSelected){ //Anima not Selected
                        monster.SelectAnima(1);
                        _cardManager.SelectAnother(monster);
                        return;
                    }

                    if(!monster.ModeSelected){ //Mode not selected
                        monster.SelectMode();
                        _cardManager.SelectAnother(monster);
                        return;
                    }

                    if(!monster.FaceSelected){ //Face not selected
                        monster.SelectFace();
                        _cardManager.StatSelectionEnd();
                        return;
                    }
                }
            }
        }

        public void Option2_Clicked(Card card){
            if(card is MonsterCard){
                var monster = card as MonsterCard;
                if(monster.FusionedCard){

                    if(!monster.AnimaSelected){
                        monster.SelectAnima(2);
                        _cardManager.SelectAnother(monster);
                        return;
                    }

                    if(!monster.ModeSelected){
                        monster.SelectMode();
                        monster.SetDeffenseMode();
                        _cardManager.StatSelectionEnd();
                        return;
                    }

                }else{

                    if(!monster.AnimaSelected){ //Anima not Selected
                        monster.SelectAnima(2);
                        _cardManager.SelectAnother(monster);
                        return;
                    }

                    if(!monster.ModeSelected){ //Mode not selected
                        monster.SelectMode();
                        monster.SetDeffenseMode();
                        _cardManager.SelectAnother(monster);
                        return;
                    }

                    if(!monster.FaceSelected){ //Face not selected
                        monster.SelectFace();
                        monster.SetFaceDown();
                        _cardManager.StatSelectionEnd();
                        return;
                    }
                }
            }
        }
    }
}