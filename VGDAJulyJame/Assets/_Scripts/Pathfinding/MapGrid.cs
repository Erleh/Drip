using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField]
    private LayerMask unpassableMask;
    [SerializeField]
    private Vector2 gridSize;
    [SerializeField]
    private float nodeRadius;
    [SerializeField]
    private bool showGizmos;

    //temp for testing
    [Header("Needed only for gizmos")]
    [SerializeField]
    private Transform player;

    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    // creates game grid
    void CreateGrid()
    {
        // sets double array for grid
        grid = new Node[gridSizeX, gridSizeY];
        // finds location of bottom left node
        Vector2 gridBottomLeft = (Vector2)transform.position - Vector2.right * gridSize.x/2 - Vector2.up * gridSize.y/2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                // set location of current node and whether or not it is an obstacle
                Vector2 currentLoc = gridBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) 
                                    + Vector2.up * (y * nodeDiameter + nodeRadius);

                bool passable = !(Physics2D.OverlapCircle(currentLoc, nodeRadius, unpassableMask));

                grid[x, y] = new Node(passable, currentLoc, x, y);
            }
        }
    }

    public List<Node> GetNeighbourNodes(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // search a 3x3 area around given node to find neighboring nodes
        // using given node as center point
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && 
                   checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    // return location of world point to node point
    public Node WorldToNodePoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x + gridSize.x / 2) / gridSize.x;
        float percentY = (worldPosition.y + gridSize.y / 2) / gridSize.y;

        // insures values are clamped between 0 and 1
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // find index of node through multiplying percent with total size - 1 to account for index 0
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        //print(x + "," + y);
        return grid[x, y];
    }

    // test
    [SerializeField]
    private Transform enemy;

    //public List<Node> path;
    // draw representation for visual while editing
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridSize.x, gridSize.y));

        if(grid != null && showGizmos)
        {
            // temp player
            Node playerNode = WorldToNodePoint(player.position);

            Node enemyNode = WorldToNodePoint(enemy.position);

            foreach(Node n in grid)
            {
                // if node is not passable, make visual red, else white
                Gizmos.color = (n.passable) ? Color.white : Color.red;

                /*
                if (path != null)
                {
                    if(path.Contains(n))
                        Gizmos.color = Color.blue;
                }
                */

                if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }

                //temp
                if(enemyNode == n)
                {
                    Gizmos.color = Color.yellow;
                }

                Gizmos.DrawCube(n.worldPos, Vector2.one * (nodeDiameter - .1f));
            }
        }
    }
}
