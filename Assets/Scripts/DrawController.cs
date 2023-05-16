using UnityEngine;

public class DrawController : MonoBehaviour
{
    [SerializeField]
    private Collider _blackboardCollider;
    [SerializeField]
    private float _rayDistance = 100;
    
    private Material _cursorMaterial;
    private LineRenderer _lineRenderer;
    private Camera _camera;

    private void Awake()
    {
        _cursorMaterial = GetComponent<MeshRenderer>().material;
        _lineRenderer = GetComponent<LineRenderer>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }

    private void Update()
    {
        MoveCursorAtPoint();
    }

    public void SetColor(Color color)
    {
        _cursorMaterial.color = color;
        
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
    }
    
    private void MoveCursorAtPoint()
    {
        var mousePosition = Input.mousePosition;
        var ray = _camera.ScreenPointToRay(mousePosition);
        
        if (!_blackboardCollider.Raycast(ray, out var hitInfo, _rayDistance)) return;

        var blackboardPoint = hitInfo.point;
        transform.position = blackboardPoint;

        if (Input.GetMouseButton(0) && _lineRenderer != null)
        {
            Draw(blackboardPoint);
        }
    }
    
    private void Draw(Vector3 point)
    {
        var index = ++_lineRenderer.positionCount - 1;
        _lineRenderer.SetPosition(index, point);
    }

    private void OnDisable()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
}