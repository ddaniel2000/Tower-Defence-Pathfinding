using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool _isPlacebale;

    [SerializeField] private Tower _tower;
    public bool IsPlaceable { get { return _isPlacebale; } }

    private GridManager _gridManager;
    Pathfinder _pathfinder;

    private Vector2Int _coordinates = new Vector2Int();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _pathfinder = FindObjectOfType<Pathfinder>();
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
        if (_gridManager.GetNode(_coordinates).isWalkable && !_pathfinder.WillBlockPath(_coordinates) )
        {
            bool isSuccesfulPlaced = _tower.CreateTower(_tower, transform.position);

            if(isSuccesfulPlaced )
            {
                _gridManager.BlockNode(_coordinates);
                _pathfinder.NotifyRecivers();
            }
        
        }
    }

}
