using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {
    [SerializeField] private Renderer _boardRenderer;

    public void ChangeBattleFieldBackground(Texture2D newIlustration){
        var fieldMat = new Material(_boardRenderer.sharedMaterials[1]);
        fieldMat.SetTexture("_Background", newIlustration);

        _boardRenderer.materials = new[] { _boardRenderer.sharedMaterials[0], fieldMat,};
    }
}