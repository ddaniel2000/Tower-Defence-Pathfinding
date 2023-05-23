using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Tile> _path = new List<Tile>();
    [SerializeField] [Range(0f, 5f)] private float _movementSpeed = 1;
    private Enemy _enemy;

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void FindPath()
    {
        _path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path"); 
        foreach ( Transform child in parent.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();
            if(waypoint != null)
            {
                _path.Add(waypoint);
            }
            
        }
    }

    private void ReturnToStart()
    {
        transform.position = _path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (Tile waypoint in _path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0;

            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * _movementSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }

        _enemy.StealGold();
        gameObject.SetActive(false);
    }
}
