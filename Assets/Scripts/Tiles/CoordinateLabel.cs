using TMPro;
using UnityEngine;


[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] private Color _defoultColor = Color.white;
    [SerializeField] private Color _blockedColor = Color.gray;

    private TextMeshPro _label;
    private Vector2Int _coordinate = new Vector2Int();

    private Waypoint _waypoint;
    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        _waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
        //_label.enabled = false;
        _label.enabled = true;
        
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            updateObjectName();
        }
        SetLabelColor();
        ToggleLabels();
    }

    private void DisplayCoordinates()
    {
        _coordinate.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        _coordinate.y = Mathf.RoundToInt(transform.parent.position.z / 10);
        _label.text = _coordinate.x + "," + _coordinate.y;
    }

    private void updateObjectName()
    {
        transform.parent.name = _label.text;
    }

    private void SetLabelColor()
    {
        if(_waypoint.IsPlaceable)
        {
            _label.color = _defoultColor;
        }
        else
        {
            _label.color = _blockedColor;
        }
    }

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.IsActive();
        }
    }
}
