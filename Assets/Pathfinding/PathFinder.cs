using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int endCoordinates;
    [SerializeField] List<Node> path;

    Node startNode;
    Node endNode;
    Node currentSearchNode;

    Dictionary<Vector2Int, Node> reachedNodes = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();
    
    Vector2Int[] directionOrder = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    void Start()
    {
        startNode = grid[startCoordinates];
        endNode = grid[endCoordinates];

        BreadthFirstSeach();
        path = CreatePath();

    }

    void BreadthFirstSeach()
    {
        bool isRunning = true;
        
        frontier.Enqueue(startNode);
        reachedNodes.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            if(currentSearchNode.coordinates == endCoordinates)
            {
                isRunning = false;
            }
            ExploreNeighbors();
            Debug.Log("SIZE: " + frontier.Count);
        }
    }

    void ExploreNeighbors()
    {
        List<Node> neighgoursList = new List<Node>();

        foreach (Vector2Int direction in directionOrder)
        {
            Vector2Int newNodeCoordinates = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(newNodeCoordinates))
            {
                Node neighbour = grid[newNodeCoordinates];
                if (neighbour != null)
                {
                    neighgoursList.Add(neighbour);
                }
            }            
        }

        //Add neighbours to the reachedNodes and frontier
        foreach(Node neighbour in neighgoursList)
        {
            if((!reachedNodes.ContainsKey(neighbour.coordinates)) && neighbour.isWalkable)
            {
                neighbour.connectedTo = currentSearchNode;//Connects to previous node
                reachedNodes.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            } 
        }
    }

    List<Node> CreatePath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while((currentNode.coordinates != startNode.coordinates) || (currentNode.connectedTo == null))
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath=true;
        }

        path.Reverse();
        return path;

    }
}
