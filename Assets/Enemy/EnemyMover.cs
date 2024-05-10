using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] float movSpeed = 1f;

    GridManager gridManager;
    PathFinder pathFinder;
    Enemy enemy;

    List<Node> path = new List<Node>();


    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates;
        
        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            Debug.Log("Coordenadas a posição: " + coordinates);
        }
        
        StopAllCoroutines();
        if (path != null)
        {
            path.Clear();
        }
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());

    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            transform.LookAt(endPosition);
            float lerpFactor = 0f;
            while (lerpFactor < 1f)
            {
                Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, lerpFactor);
                transform.position = newPosition;
                lerpFactor += Time.deltaTime * movSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    private void FinishPath()
    {
        enemy.WithdrawBank();
        gameObject.SetActive(false);
    }
}
