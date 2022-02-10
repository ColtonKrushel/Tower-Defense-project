using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlacable;
    public bool IsPlacable { get { return isPlacable; } }

    GridManager gridManager;
    Pathfinding pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.getCoordinatesFromposition(transform.position);

            if (!isPlacable)
            {
                gridManager.blockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccesful = towerPrefab.createTower(towerPrefab, transform.position);
            if (isSuccesful)
            {
                gridManager.blockNode(coordinates);
                pathfinder.NotifyRecievers();
            }
        }
    }
}
