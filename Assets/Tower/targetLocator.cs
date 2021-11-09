using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float maxRange = 15f;
    ParticleSystem particles;
    Transform target;

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = maxRange;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            
            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void AimWeapon()
    {
        var em = particles.emission;

        if (target)
        {
            float TargetDistance = Vector3.Distance(transform.position, target.position);

            transform.LookAt(target);
            em.enabled = true;
        }
        else
        {
            
            em.enabled = false;
        }
    }
}
