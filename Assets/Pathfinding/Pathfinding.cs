using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    public Vector2Int StartCoordinate { get { return startCoordinate; } }

    [SerializeField] Vector2Int endCoordinate;
    public Vector2Int EndCoordinate { get { return endCoordinate; } }

    Node startNode;
    Node endNode;
    Node currentSearchNode;

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.grid;
            startNode = grid[startCoordinate];
            endNode = grid[endCoordinate];
            
        }
    }
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinate);
    }
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.resetNodes();
        BreadthFirstSearch(coordinates);
        return buildPath();
    }

    void ExploreNeighbours()
    {
        List<Node> Neighbours = new List<Node>();
        foreach(Vector2Int direction in directions) 
        {

            Vector2Int temp = direction + currentSearchNode.coordinates;
            if(grid.ContainsKey(temp))
            {
                Neighbours.Add(grid[temp]);

                grid[temp].isExplored = true;
                grid[currentSearchNode.coordinates].isPath = true;
            }
        }

        foreach(Node neighbour in Neighbours)
        {
            if(!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectedTo = currentSearchNode;
                reached.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();
            if(currentSearchNode.coordinates == endCoordinate)
            {
                isRunning = false;
            }
        }
    }
    List<Node> buildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyRecievers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
