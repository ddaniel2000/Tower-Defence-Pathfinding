using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool _isPlacebale;

    [SerializeField] private Tower _tower;
    public bool IsPlaceable { get { return _isPlacebale; } }

    private void OnMouseDown()
    {
        if (_isPlacebale)
        {
            bool isPlaced = _tower.CreateTower(_tower, transform.position);

            _isPlacebale = !isPlaced;

        }
    }

}
