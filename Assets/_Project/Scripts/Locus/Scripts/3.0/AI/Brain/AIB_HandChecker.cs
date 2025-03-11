using System.Collections.Generic;
using UnityEngine;
namespace Mistix{
    public class AIB_HandChecker : MonoBehaviour {
        public List<MonsterCard> Lvl2OnHand { get; private set; } = new();
        public List<MonsterCard> Lvl3OnHand { get; private set; } = new();
        public List<MonsterCard> Lvl4OnHand { get; private set; } = new();

        public void OrganizeCardsOnHand(List<Card> cardsInHand){
            ClearHandLists();

            foreach (var card in cardsInHand){
                if (card is MonsterCard){
                    int lvl = (card as MonsterCard).Level;

                    if (lvl == 2){
                        Lvl2OnHand.Add(card as MonsterCard);
                    }

                    if (lvl == 3){
                        Lvl3OnHand.Add(card as MonsterCard);
                    }

                    if (lvl == 4){
                        Lvl4OnHand.Add(card as MonsterCard);
                    }
                }
            }
        }

        public void ClearHandLists(){
            Lvl2OnHand.Clear();
            Lvl3OnHand.Clear();
            Lvl4OnHand.Clear();
        }
    }
}