using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;

    public bool createTower(Tower tower, Vector3 position)
    {
        bank bank = FindObjectOfType<bank>();

        if(bank == null)
        {
            return false;
        }

        if(bank.GetCurrentBalance >= cost)
        {
            bank.Withdraw(cost);
            return (Instantiate(tower, position, Quaternion.identity));
        }

        return false;
    }
}
