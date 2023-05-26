using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed
}
public class MazeNode : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private MeshRenderer _floor;


    /// <summary>
    /// State changes of the floor
    /// </summary>
    /// <param name="state"></param>

    public void RemoveWall(int wallToRemove)
    {
        _walls[wallToRemove].gameObject.SetActive(false);
    }

    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Available:
                _floor.material.color = Color.white;
                break;
            case NodeState.Current:
                _floor.material.color = Color.yellow;
                break;
            case NodeState.Completed:
                _floor.material.color = Color.blue;
                break;
        }
    }
}
