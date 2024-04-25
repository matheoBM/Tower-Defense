using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;     //
    public bool isExplored;     //Was already checked in pathfinding algorithm
    public bool isPath;         //Part of the path finded
    public Node connectedTo;    //Next node

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
