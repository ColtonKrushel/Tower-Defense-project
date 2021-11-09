using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyValue = 25;
    [SerializeField] int enemyPenalty = 25;

    bank Bank;

    // Start is called before the first frame update
    void Start()
    {
        Bank = FindObjectOfType<bank>();
    }

    public void RewardGold()
    {
        if(Bank == null) { return; }
        Bank.Deposit(enemyValue);
    }

    public void StealGold()
    {
        if (Bank == null) { return; }
        Bank.Withdraw(enemyPenalty);
    }
}

