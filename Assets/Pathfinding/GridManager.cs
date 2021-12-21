using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int GridSize;
    [SerializeField] int worldGridSize = 10;
    public int WorldGridSize { get { return worldGridSize; } }

    Dictionary<Vector2Int, Node> Grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> grid { get { return Grid;  } }

    void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if(Grid.ContainsKey(coordinates))
        {
            return Grid[coordinates];
        }

        return null;
    }

    public void blockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public Vector2Int getCoordinatesFromposition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / worldGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / worldGridSize);

        return coordinates;
    }

    public Vector3 getPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * worldGridSize;
        position.z = coordinates.y * worldGridSize;

        return position;
    }

    void CreateGrid()
    {
        for(int x = 0; x < GridSize.x; x++)
        {
            for(int y = 0; y < GridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                Grid.Add(coordinates, new Node(coordinates, true));

            }
        }
    }
}
