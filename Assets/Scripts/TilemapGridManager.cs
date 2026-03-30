using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TilemapGridManager : MonoBehaviour
{
    public Tilemap groundTilemap; // walkable tiles
    public Tilemap wallTilemap;   // unwalkable tiles

    public Dictionary<Vector3Int, Node> grid = new Dictionary<Vector3Int, Node>();

    void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid.Clear();

        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (!groundTilemap.HasTile(pos))
                continue;

            bool walkable = !wallTilemap.HasTile(pos);
            Vector3 worldPos = groundTilemap.GetCellCenterWorld(pos);

            grid[pos] = new Node(walkable, worldPos, pos);
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3Int cellPos = groundTilemap.WorldToCell(worldPosition);
        return grid.ContainsKey(cellPos) ? grid[cellPos] : null;
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        Vector3Int[] directions =
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right,
            new Vector3Int(1,1,0),
            new Vector3Int(-1,1,0),
            new Vector3Int(1,-1,0),
            new Vector3Int(-1,-1,0)
        };

        foreach (Vector3Int dir in directions)
        {
            Vector3Int checkPos = node.cellPosition + dir;

            if (!grid.ContainsKey(checkPos))
                continue;

            // prevent diagonal corner cutting
            if (dir.x != 0 && dir.y != 0)
            {
                Vector3Int sideA = node.cellPosition + new Vector3Int(dir.x, 0, 0);
                Vector3Int sideB = node.cellPosition + new Vector3Int(0, dir.y, 0);

                if (!grid.ContainsKey(sideA) || !grid.ContainsKey(sideB))
                    continue;

                if (!grid[sideA].walkable || !grid[sideB].walkable)
                    continue;
            }

            neighbours.Add(grid[checkPos]);
        }

        return neighbours;
    }

    /*void OnDrawGizmos()
    {
        if (grid == null) return;

        foreach (Node node in grid.Values)
        {
            Gizmos.color = node.walkable ? Color.white : Color.red;
            Gizmos.DrawWireCube(node.worldPosition, Vector3.one);
        }
    }*/
}
