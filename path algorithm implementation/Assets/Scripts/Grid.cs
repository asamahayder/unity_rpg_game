using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public bool onlyDisplayPathGizmos;

    public LayerMask unwalkableMask;
    Node[,] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public Transform player;



    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (onlyDisplayPathGizmos)
        {
            if(path != null)
            {
                foreach(Node node in path)
                {
                    if (path.Contains(node)) Gizmos.color = Color.black;
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
        else
        {
            if (grid != null)
            {

                foreach (Node node in grid)
                {
                    Gizmos.color = (node.walkable) ? Color.white : Color.red;
                    if (path != null)
                    {
                        if (path.Contains(node)) Gizmos.color = Color.black;
                    }
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        //first we find how many nodes we can fit inside the grid.
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //we use Mathf to round to integer because we cant have halv nodes etc.
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();



    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }

    }

    public Node nodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX); //makes sure that our percentage value is always between 0 and 1. (like in case the character is outside the map)
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];

    }

    public List<Node> getNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; //we skip this position because it is in fact the node itself and therefore not a neighbor

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >=0 && checkY < gridSizeY) //checking if the current neighbor is inside the grid
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }

        }

        return neighbors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
