namespace Mistix{
    using UnityEngine;
    
    public class BattleManager : MonoBehaviour {
        [field:SerializeField] public BoardManager BoardManager { get; private set; }
        
        public void UpdateUI(){
            Debug.Log("UpdateUI");
        }
    }
}