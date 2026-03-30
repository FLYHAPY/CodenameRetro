using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public Vector3Int cellPosition;

    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost => gCost + hCost;

    public Node(bool walkable, Vector3 worldPos, Vector3Int cellPos)
    {
        this.walkable = walkable;
        worldPosition = worldPos;
        cellPosition = cellPos;
    }
}