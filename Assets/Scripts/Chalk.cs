using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Chalk : MonoBehaviour
{
    [SerializeField]
    private DrawController _drawController;
    
    [UsedImplicitly]
    public void OnClick()
    {
        var color = GetComponent<Image>().color;
        _drawController.SetColor(color);
    }
}