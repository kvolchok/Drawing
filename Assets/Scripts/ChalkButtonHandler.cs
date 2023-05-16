using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChalkButtonHandler : MonoBehaviour
{
    [SerializeField]
    private LineDrawingController _lineDrawingController;
    [SerializeField]
    private DrawController _drawController;

    private void Start()
    {
        var color = GetComponent<Image>().color;
        _drawController.SetColor(color);
    }

    public void ActivateChosenLineDrawing()
    {
        _lineDrawingController.DisableAllLineDrawings();
        _drawController.enabled = true;
    }
}