using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 0.5f;

    void Start()
    {
        StartCoroutine(Build());
    }

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

    IEnumerator Build()
    {
        foreach(Transform part in transform)
        {
            part.gameObject.SetActive(false);
            foreach(Transform grandchild in part)
            {
                grandchild.gameObject.SetActive(false);
            }
        }
        foreach (Transform part in transform)
        {
            part.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandchild in part)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
