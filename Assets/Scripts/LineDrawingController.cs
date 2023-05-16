using System.Collections.Generic;
using UnityEngine;

public class LineDrawingController : MonoBehaviour
{
    [SerializeField]
    private List<DrawController> _lineDrawings;

    public void DisableAllLineDrawings()
    {
        foreach (var lineDrawing in _lineDrawings)
        {
            lineDrawing.enabled = false;
        }
    }

    public void ClearBlackboard()
    {
        var lineRenderers = GetComponentsInChildren<LineRenderer>();
        foreach (var lineRenderer in lineRenderers)
        {
            lineRenderer.positionCount = 0;
        }
    }
}