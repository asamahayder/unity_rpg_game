using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PathFinding : MonoBehaviour
{

    public Transform seeker, target;
    public LayerMask groundMask;

    public GameObject waypoint;
    private GameObject currentWayPoint;

    Grid grid;
    ObjectMover mover;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        mover = GetComponent<ObjectMover>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            findPath(seeker.position, GetMouseClickWorldPosition());

        }

    }

    private Vector3 GetMouseClickWorldPosition()
    {
        Vector3 clickPosition = -1000 * Vector3.one;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, groundMask))
        {
            clickPosition = hit.point;
        }

        return clickPosition;

    }


    void findPath(Vector3 startPosition, Vector3 targetPosition)
    {
        

        Stopwatch sw = new Stopwatch();
        sw.Start();

        Node startNode = grid.nodeFromWorldPoint(startPosition);
        Node targetNode = grid.nodeFromWorldPoint(targetPosition);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.add(startNode);


        
        while (openSet.count > 0)
        {
            Node node = openSet.RemoveFirst();
            closedSet.Add(node);

            if (node == targetNode) //we have found destination
            {
                sw.Stop();
                print("Path found: " + sw.ElapsedMilliseconds + " ms");

                if (currentWayPoint != null) Destroy(currentWayPoint);
                instantiateWaypoint(targetPosition);

                retracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbor in grid.getNeighbors(node))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor)) continue; //we skip non walkable nodes and nodes in the closed set
                
                int newMovementCostToNeighbor = node.gCost + getDistance(node, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = getDistance(neighbor, targetNode);
                    neighbor.parent = node;

                    if (!openSet.contains(neighbor))
                    {
                        openSet.add(neighbor);
                    }
                }

            }

        }
    }

    void retracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
        mover.updatePath(path);

    }

    

    int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY) return 14 * distanceY + 10 * (distanceX - distanceY);
        //else
        return 14 * distanceX + 10 * (distanceY - distanceX);

    }

    void instantiateWaypoint(Vector3 position)
    {
        Node node = grid.nodeFromWorldPoint(position);

        Vector3 waypointPosition = new Vector3(node.worldPosition.x, position.y+1, node.worldPosition.z);

        currentWayPoint = Instantiate(waypoint, waypointPosition, new Quaternion());
    }
}
