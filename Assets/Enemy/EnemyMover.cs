using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0,5)]float speed = 1f;

    List<Node> Keypath = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    Pathfinding pathfinder;


    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(PrintWaypointName());
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();
    }

    void RecalculatePath()
    {
        Keypath.Clear();
        Keypath = pathfinder.GetNewPath();
    }

    void ReturnToStart()
    {
        transform.position = gridManager.getPositionFromCoordinates(pathfinder.StartCoordinate);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator PrintWaypointName()
    {
        for(int i = 0; i < Keypath.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.getPositionFromCoordinates(Keypath[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}
