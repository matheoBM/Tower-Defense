using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem arrowParticles;

    Transform target;

    void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;
    }

    void Update()
    {
        FindClosestEnemy();
        AimWeapon();
    }

    void FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestEnemy = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float distance = 0;
            distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < maxDistance)
            {
                maxDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        target = closestEnemy;
    }

    private void AimWeapon()
    {
        float distance = Vector3.Distance(transform.position, target.position);
  
        weapon.LookAt(target);
        ToggleAttack(distance <= range);
    }

    void ToggleAttack(bool isActive)
    {
        var emissionModule = arrowParticles.emission;
        emissionModule.enabled = isActive;
    }
}
