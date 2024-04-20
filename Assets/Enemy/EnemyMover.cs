using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f, 10f)] float movSpeed = 1f;

    Enemy enemy;

    void Awake()
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
                path.Add(child.GetComponent<Waypoint>());
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            transform.LookAt(endPosition);
            float lerpFactor = 0f;
            while(lerpFactor < 1f)
            {
                Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, lerpFactor);
                transform.position = newPosition;
                lerpFactor += Time.deltaTime * movSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
        enemy.WithdrawBank();
        gameObject.SetActive(false);
    }
}
