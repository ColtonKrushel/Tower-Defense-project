using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlacable;
    public bool getIsPlacable()
    {
        return isPlacable;
    }

    private void OnMouseDown()
    {
        if (isPlacable){
            bool isPlaced = towerPrefab.createTower(towerPrefab, transform.position);
            isPlacable = !isPlaced;
        }
    }
}
