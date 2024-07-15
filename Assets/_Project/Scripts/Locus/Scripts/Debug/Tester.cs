using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tester : MonoBehaviour {
    public static Tester Instance;

    public HelperSO Helper;

    public CardManagerSO CardManager;
    public ColorManagerSO ColorManager;
    public List<CardSO> Cards;

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

    private void Awake() {
        if(Instance != null){
            Helper.RedDebug("More than one instance of", "Tester");
            Destroy(Instance);
        }
        Instance = this;
    }

    Card instanciatedCard;

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
            instanciatedCard.Visuals.Dissolve.SolidifyCard(ColorManager.Moon);
        }
    }
}