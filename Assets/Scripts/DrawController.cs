using System;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    [SerializeField]
    private Collider _blackboardCollider;
    [SerializeField]
    private float _rayDistance = 100;

    [SerializeField]
    private LineRenderer _linePrefab;

    private List<LineRenderer> _lines = new();

    private Material _cursorMaterial;
    private LineRenderer _line;
    private Camera _camera;
    private Color _color;
    private bool _isDrawing;

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
        MoveCursorAtPoint();
    }

    private void MoveCursorAtPoint()
    {
        var mousePosition = Input.mousePosition;
        var ray = _camera.ScreenPointToRay(mousePosition);
        
        if (!_blackboardCollider.Raycast(ray, out var hitInfo, _rayDistance)) return;

        var blackboardPoint = hitInfo.point;
        transform.position = blackboardPoint;

        if (Input.GetMouseButton(0) && _line != null)
        {
            Draw(blackboardPoint);
        }
    }

    private void StartDrawing()
    {
        _line = Instantiate(_linePrefab);
        _line.material.color = _color;
        _lines.Add(_line);
        _isDrawing = true;
    }

    private void Draw(Vector3 point)
    {
        var index = ++_line.positionCount - 1;
        _line.SetPosition(index, point);
    }
}