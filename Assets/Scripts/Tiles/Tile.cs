using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool _isPlacebale;

    [SerializeField] private Tower _tower;
    public bool IsPlaceable { get { return _isPlacebale; } }

    private GridManager _gridManager;

    private Vector2Int _coordinates = new Vector2Int();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        if(_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
            if(!_isPlacebale)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (_isPlacebale)
        {
            bool isPlaced = _tower.CreateTower(_tower, transform.position);

            _isPlacebale = !isPlaced;

        }
    }

}
