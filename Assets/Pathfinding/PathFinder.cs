using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int endCoordinates;
    public Vector2Int EndCoordinates { get { return endCoordinates; } }

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
            startNode = grid[startCoordinates];
            endNode = grid[endCoordinates];
        }
    }

    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        //Reset Grid and Search lists
        gridManager.ResetGrid();

        BreadthFirstSeach(coordinates);
        return CreatePath();
    }

    void BreadthFirstSeach(Vector2Int coordinates)
    {
        grid[coordinates].isWalkable = true;
        endNode.isWalkable = true; //Garantee that these nodes are walkable for the path finder;
        frontier.Clear();
        reachedNodes.Clear();

        bool isRunning = true;
        
        frontier.Enqueue(grid[coordinates]);
        reachedNodes.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            if(currentSearchNode.coordinates == endCoordinates)
            {
                isRunning = false;
            }
            ExploreNeighbors();
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

        while((currentNode.connectedTo != null))
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath=true;
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
            List<Node> path = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(path.Count <= 1)
            {
                GetNewPath();
                return true;
            }

        }
        return false;
    }

    public void NotifyReceivers()
    {
        Debug.Log("Notificando");
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.RequireReceiver);
    }
}
