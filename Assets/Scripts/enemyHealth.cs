using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class enemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] int difficultyRamp = 1;
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
            maxHealth += difficultyRamp;
            enemy.RewardGold();
            currentHitPoints = maxHealth;
        }
    }
}
