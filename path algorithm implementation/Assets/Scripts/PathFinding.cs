using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PathFinding : MonoBehaviour
{

    public Transform seeker;
    public LayerMask groundMask;

    public GameObject waypoint;
    private GameObject currentWayPoint;

    private Grid grid;
    public GameObject gridObject;

    ObjectMover mover;

    //For roaming
    public bool activateRoam = false;
    private Vector3 startPosition;
    public float roamingRadius = 10f;
    private float z1,z2,x1,x2;
    private bool hasWaited = false;

    private void Awake()
    {
        grid = gridObject.GetComponent<Grid>();

        mover = GetComponent<ObjectMover>();
    }

    private void Start()
    {
        startPosition = seeker.position;
        z1 = startPosition.z - roamingRadius;
        z2 = startPosition.z + roamingRadius;
        x1 = startPosition.x - roamingRadius;
        x2 = startPosition.x + roamingRadius;

        if (activateRoam) StartCoroutine(roam());
    }

    private void Update()
    {
        if (!activateRoam && Input.GetMouseButtonDown(0))
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

    IEnumerator roam()
    {
       
        while (true)
        {
            
            if (mover.getIsAtEndOfPath())
            {
                //return here?
                if (!hasWaited)
                {
                    hasWaited = true;
                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    hasWaited = false;
                    //Find a random target and pathfind to it
                    Vector3 randomPositionWithinRoamArea = new Vector3(Random.Range(x1, x2), 0, Random.Range(z1, z2));

                    Node targetNode = grid.nodeFromWorldPoint(randomPositionWithinRoamArea);

                    //find path
                    findPath(seeker.position, targetNode.worldPosition);
                }
                
            }


            if (!mover.getIsAtEndOfPath())
            {
                yield return null;
            }

        }
    }

}
