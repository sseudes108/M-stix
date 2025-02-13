using System.Collections.Generic;
using UnityEngine;

namespace Mistix{

    public class BoardPlaceVisualController{
        public void LightUpPlaces(List<BoardPlace> boardPlaces, Color color){
            foreach(var place in boardPlaces){
                place.LightUp(color);
            }
        }

        public void LightOffPlaces(List<BoardPlace> boardPlaces, Color color) {
            foreach(var place in boardPlaces){
                place.LightOff(color);
            }
        }

        public void HighlightPlace(BoardPlace boardPlace){
            boardPlace.HighLight();
        }
    }
}