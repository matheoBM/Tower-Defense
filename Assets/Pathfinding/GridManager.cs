using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Start()
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
                Debug.Log("Coordinate: " + coordinate + "; Node: " + grid[coordinate].coordinates);
            }
        }
    }

}
