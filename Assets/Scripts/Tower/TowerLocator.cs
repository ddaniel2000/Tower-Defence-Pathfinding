using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLocator : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private ParticleSystem _projectileParticles;
    [SerializeField] private float _range = 15; // 1 tile and a half
    private Transform _target;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
        
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float _maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance < _maxDistance)
            {
                closestTarget = enemy.transform;
                _maxDistance = targetDistance;
            }
        }
        _target = closestTarget;
    }

    private void AimWeapon()
    {
        if(_target == null)
        {
            return;
        }
        float targetDistance = Vector3.Distance(transform.position, _target.position);

        _weapon.LookAt(_target);

        if(targetDistance < _range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    private void Attack(bool isActive)
    {
        var emissionModule = _projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
