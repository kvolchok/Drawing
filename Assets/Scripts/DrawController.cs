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

    private Camera _camera;
    private Material _cursorMaterial;
    private LineRenderer _line;
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
        if (_color == null)
        {
            return;
        }

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
        
        MoveCursorAtPoint();
    }

    private void StartDrawing()
    {
        _line = Instantiate(_linePrefab);
        _line.material.color = _color;
        _lines.Add(_line);
        _isDrawing = true;
    }

    private void EndDrawing()
    {
        _isDrawing = false;
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
    
    private void Draw(Vector3 point)
    {
        var index = ++_line.positionCount - 1;
        _line.SetPosition(index, point);
    }
}