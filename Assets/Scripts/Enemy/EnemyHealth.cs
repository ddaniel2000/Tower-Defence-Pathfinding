using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHitsPoints = 5;

    [Tooltip("Adds ammount to maxHitPoints when enemy dies.")]
    [SerializeField] private int _difficultyRamp = 1;

    private int _currentHits = 0;
    private Enemy _enemy;

    [SerializeField] private ShowDamage _displayDamage;

    // Start is called before the first frame update
    void OnEnable()
    {
        _currentHits = _maxHitsPoints;
        DisplayDamage();

    }
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        DisplayDamage();
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeHit();
    }

    private void TakeHit()
    {
        _currentHits--;
        DisplayDamage();
        if (_currentHits <= 0)
        {
            
            gameObject.SetActive(false);
            _maxHitsPoints += _difficultyRamp;
            _enemy.RewardGold();
            
        }
    }

    private void DisplayDamage()
    {
        _displayDamage.DisaplyDamage(_currentHits, _maxHitsPoints);
    }
}
