// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;

// public class UICardPlaceHolder : MonoBehaviour{
//     private Renderer _renderer;

//     [SerializeField] private Renderer _animaPlaceHolder;
//     [SerializeField] private TextMeshProUGUI _anima1;
//     [SerializeField] private TextMeshProUGUI _anima2;
//     [SerializeField] private TextMeshProUGUI _lvl;
//     [SerializeField] private TextMeshProUGUI _atk;
//     [SerializeField] private TextMeshProUGUI _def;

//     [SerializeField] private Movement _movement;

//     private void Awake() {
//         _renderer = GetComponentInChildren<Renderer>();
//         _movement = GetComponentInChildren<Movement>();
//     }

//     public void ChangeIllustration(Texture2D newIlustration){
//         var faceMat = new Material(_renderer.sharedMaterials[1]);
//         faceMat.SetTexture("_Ilustration", newIlustration);

//         _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };

//         _animaPlaceHolder.gameObject.SetActive(false);

//         _anima1.text = " ";
//         _anima2.text = " ";
//         _lvl.text = " ";
//         _atk.text = " ";
//         _def.text = " ";
//     }
//     public void ChangeIllustration(Texture2D newIlustration, List<EAnimaType> animas, int lvl, int atk, int def){
//         var faceMat = new Material(_renderer.sharedMaterials[1]);
//         faceMat.SetTexture("_Ilustration", newIlustration);
//         _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };

//         _animaPlaceHolder.gameObject.SetActive(true);
//         var animaHolderMat = new Material(_animaPlaceHolder.material);
//         animaHolderMat.SetColor("_Anima1Color", BattleManager.Instance.ColorManager.GetAnimaColor(animas[0]));
//         animaHolderMat.SetColor("_Anima2Color", BattleManager.Instance.ColorManager.GetAnimaColor(animas[1]));
//         _animaPlaceHolder.material = animaHolderMat;
        
//         _anima1.text = animas[1].ToString();
//         _anima2.text = animas[0].ToString();

//         _lvl.text = @$"Level    {lvl}";
//         _atk.text = $"Atk   {atk}";
//         _def.text = $"Def   {def}";
//     }

//     public Movement Movement => _movement;
// }