using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlacable;
    public bool IsPlacable { get { return isPlacable; } }

    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.getCoordinatesFromposition(transform.position);

            if (isPlacable)
            {
                gridManager.blockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlacable){
            bool isPlaced = towerPrefab.createTower(towerPrefab, transform.position);
            isPlacable = !isPlaced;
        }
    }
}
