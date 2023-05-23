using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    [SerializeField] private Vector2Int _startCoordinates;
    public Vector2Int StartCoordinates {  get { return _startCoordinates; } }

    [SerializeField] private Vector2Int _destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return _destinationCoordinates; } }

    private Node _startNode;
    private Node _destinationNode;
    private Node _currentSearchNode;

    private Queue<Node> _frontier = new Queue<Node>();
    private Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();

    private Vector2Int[] _directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    private GridManager _gridManager;

    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        if(_gridManager != null)
        {
            _grid = _gridManager.Grid;

            _startNode = _grid[_startCoordinates];
            _destinationNode = _grid[_destinationCoordinates];

          
        }

    }

    void Start()
    {

        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        _gridManager.ResetNode();

        BreadhFirstSearch();
        return BuildPath();
 
    }

    private void ExploreNeigbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in _directions)
        {
            Vector2Int neighborCoords = _currentSearchNode.coordinates + direction;

            if(_grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(_grid[neighborCoords]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if(!_reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connedtedTo = _currentSearchNode;
                _reached.Add(neighbor.coordinates, neighbor);
                _frontier.Enqueue(neighbor);
            }
        }

    }

    private void BreadhFirstSearch()
    {
        _startNode.isWalkable = true;
        _destinationNode.isWalkable = true;

        _frontier.Clear();
        _reached.Clear();

        bool isRunning = true;

        _frontier.Enqueue(_startNode);
        _reached.Add(_startCoordinates, _startNode);

        while(_frontier.Count > 0 && isRunning)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode.isExplored = true;
            ExploreNeigbors();
            if(_currentSearchNode.coordinates == _destinationCoordinates)
            {
                isRunning = false;
            }
        }

    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = _destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connedtedTo != null)
        {
            currentNode = currentNode.connedtedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(_grid.ContainsKey(coordinates))
        {
            bool previousState = _grid[coordinates].isWalkable;
            _grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            _grid[coordinates].isWalkable = true;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

            return false;
        }
        return false;
    }

    public void NotifyRecivers()
    {
        BroadcastMessage("RecalculatePath",SendMessageOptions.DontRequireReceiver);
    }

}
