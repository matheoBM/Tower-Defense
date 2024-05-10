using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable = true;
    public bool IsPlaceable { get { return isPlaceable; } }
    public GridManager gridManager;
    public Vector2Int coordinates; //In terms of tile number
    PathFinder pathFinder;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();    
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(isPlaceable == false)
            {
                gridManager.BlockNode(coordinates);
            }
        }    
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isPlaced = towerPrefab.PlaceTower(towerPrefab, transform.position);
            if (isPlaced)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }
    }
}
