using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    
    [SerializeField] [Range(0f, 5f)] private float _movementSpeed = 1;

    private List<Node> _path = new List<Node>();

    private Enemy _enemy;
    private GridManager _gridManager;
    private Pathfinder _pathfinder;

    private void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _gridManager = FindObjectOfType<GridManager>();
        _pathfinder = FindObjectOfType<Pathfinder>();
        
    }

    private void RecalculatePath()
    {
        _path.Clear();
        _path = _pathfinder.GetNewPath();
    }

    private void ReturnToStart()
    {
        transform.position = _gridManager.GetPositionFromCoordinates(_pathfinder.StartCoordinates);
    }


    private void FinishPath()
    {
        _enemy.StealGold();
        gameObject.SetActive(false);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 0; i < _path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = _gridManager.GetPositionFromCoordinates(_path[i].coordinates);
            float travelPercent = 0;

            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * _movementSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }

        FinishPath();
    }

}
