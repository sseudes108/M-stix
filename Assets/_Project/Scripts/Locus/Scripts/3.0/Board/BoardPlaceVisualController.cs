namespace Mistix{
    using System.Collections.Generic;
    
    public class BoardPlaceVisualController{
        public void LightUpPlaces(List<BoardPlace> boardPlaces){
            foreach(var place in boardPlaces){
                place.Visual.LightUp();
            }
        }
    }
}