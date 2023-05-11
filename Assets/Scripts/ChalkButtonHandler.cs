using UnityEngine;
using UnityEngine.UI;

public class ChalkButtonHandler : MonoBehaviour
{
    [SerializeField]
    private LineDrawingController _lineDrawingController;
    [SerializeField]
    private LineDrawing _lineDrawing;

    private void Start()
    {
        var color = GetComponent<Image>().color;
        _lineDrawing.SetColor(color);
    }

    public void ActivateChosenLineDrawing()
    {
        _lineDrawingController.DisableAllLineDrawings();
        _lineDrawing.enabled = true;
    }
}