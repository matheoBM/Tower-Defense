using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Tile> path = new List<Tile>();
    [SerializeField][Range(0f, 10f)] float movSpeed = 1f;

    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();
        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");
        if (pathParent != null)
        {
            foreach(Transform child in pathParent.transform)
            {
                Tile waypoint = child.GetComponent<Tile>();
                if (waypoint != null)
                {
                    path.Add(waypoint);
                }
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach (Tile waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
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
