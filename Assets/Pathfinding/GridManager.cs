using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        else
        {
            return null;
        }
    }

    void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for(int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                Vector2Int coordinate = new Vector2Int(i, j);
                grid.Add(coordinate, new Node(coordinate, true));
            }
        }
    }

}
