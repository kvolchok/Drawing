using System.Collections.Generic;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _linePrefab;

    private readonly List<LineRenderer> _lines = new();

    private Color _color;
    private Camera _camera;
    private Material _cursorMaterial;
    private LineRenderer _line;
    private bool _isDrawing;
    private Vector3 _lastPoint;

    public void SetColor(Color color)
    {
        _color = color;
    }
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!_isDrawing && Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        
        if (_isDrawing && Input.GetMouseButtonUp(0))
        {
            EndDrawing();
        }

        if (!_isDrawing)
        {
            return;
        }
        
        Draw();
    }

    private void StartDrawing()
    {
        _line = Instantiate(_linePrefab);
        _line.material.color = _color;
        _line.sortingOrder = _lines.Count;
        _lines.Add(_line);
        _isDrawing = true;
    }

    private void EndDrawing()
    {
        _isDrawing = false;
    }

    private void Draw()
    {
        var mousePosition = Input.mousePosition;
        var ray = _camera.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out var hitInfo))
        {
            return;
        }

        var currentPoint = hitInfo.point;

        if (Vector3.Distance(_lastPoint, currentPoint) < 0.1)
        {
            return;
        }

        var index = ++_line.positionCount - 1;
        _line.SetPosition(index, currentPoint);
        
        _lastPoint = _line.GetPosition(_line.positionCount - 1);
    }
}