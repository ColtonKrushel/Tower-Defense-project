using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    int currentHitPoints = 0;

    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHealth;
        enemy = FindObjectOfType<Enemy>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;
        if(currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
