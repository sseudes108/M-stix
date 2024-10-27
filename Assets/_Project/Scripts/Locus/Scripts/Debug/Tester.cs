using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tester : MonoBehaviour{
    public static Tester Instance;
    public CardManagerSO CardManager;
    public ColorDatabaseSO ColorManager;
    public List<CardSO> Cards;
    Card instanciatedCard;

    private void Awake() {
        if(Instance != null){
            Debug.Log("More than one instance of Tester");
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.D)) {
            instanciatedCard = Instantiate(CardManager.Creator.CreateCard(Cards[Random.Range(0, Cards.Count)]));
            instanciatedCard.transform.position = new Vector3(0, 1,-8);
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            System.GC.Collect();
        }

        if(Input.GetKeyDown(KeyCode.S)){
            instanciatedCard.Visuals.Dissolve.DissolveCard(Color.magenta);
        }

        if(Input.GetKeyDown(KeyCode.I)){
            Color color = new(ColorManager.Moon.x, ColorManager.Moon.y, ColorManager.Moon.z);
            instanciatedCard.Visuals.Dissolve.SolidifyCard(color);
        }
    }

    public void CheckNullRef(Object obj){
        if(obj == null){
            Debug.Log($"{obj.name} is <color=red>Null</color=red>");
        }else{
            Debug.Log($"{obj.name} is <color=green>Is Not Null</color=green>");
        }
    }
    
    public void CheckCall(string obj, string funcionName, string color){
        Debug.Log($"<color=white><b>{obj}</b></color=white> - <color={color}>{funcionName}</color={color}>");
    }
}