using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteAlways] //Execute both in playmode and in edit mode
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white; 
    [SerializeField] Color blockedColor = Color.grey;
    [SerializeField] Color exploredColor = Color.blue;
    [SerializeField] Color pathColor = Color.red;

    TMP_Text label;
    Vector2Int coordinates = new Vector2Int();
    Tile waypoint;
    GridManager gridManager;

    void Awake()
    {
        waypoint = GetComponentInParent<Tile>();
        gridManager = FindObjectOfType<GridManager>();
    }

    void OnEnable()
    {
        label = GetComponent<TMP_Text>();
        label.enabled = true;
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        DisplayCoordinates();
        UpdateObjectName();
        //Debugging
        UpdateColorCoordinates();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }

    private void UpdateColorCoordinates()
    {
        if(gridManager == null) { return; }
        
        Node node = gridManager.GetNode(coordinates);
        if(node == null) { return; }   
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }

    }

    void DisplayCoordinates()
    {
        if (label == null)
        {
            Debug.LogWarning("TextMeshPro component not found.");
            return;
        }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.unityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.unityGridSize);
        label.text = $"{coordinates.x},{coordinates.y}";
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
