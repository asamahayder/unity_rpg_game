using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PathFinding : MonoBehaviour
{

    private Transform actorTransform;  //the current position of the NPC/Player
    public LayerMask groundMask;

    public GameObject waypoint;
    private GameObject currentWayPoint;

    private Grid grid;
    private GameObject gridObject;

    ObjectMover mover;

    //For roaming and npc
    public bool isNPC = false; //Maybe let this be controlles by NPC behavior instead of public?
    public bool activateRoam = false; //this only controlls the initial roam behavior. //Maybe move this to NPCBehavior?
    public Vector3 startPosition;
    public float roamingRadius = 10f;
    public float z1,z2,x1,x2;
    private bool hasWaited = false;

    public int numberOfRoutinesRunning = 0;

    IEnumerator roamRutine;

    private void Awake()
    {
        gridObject = GameObject.Find("Grid").gameObject;
        grid = gridObject.GetComponent<Grid>();

        mover = GetComponent<ObjectMover>();


    }

    private void Start()
    {
        roamRutine = roam();

        actorTransform = GetComponent<Transform>();

        startPosition = actorTransform.position;
        z1 = startPosition.z - roamingRadius;
        z2 = startPosition.z + roamingRadius;
        x1 = startPosition.x - roamingRadius;
        x2 = startPosition.x + roamingRadius;

        if (activateRoam && isNPC) StartCoroutine(roamRutine); //roaming only possible for npc's
    }

    private void Update()
    {

    }

    
    public void findPath(Vector3 targetPosition, bool toInteractableObject)
    {
        Vector3 startPosition = actorTransform.position;

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
                //print("Path found: " + sw.ElapsedMilliseconds + " ms");


                if (currentWayPoint != null) Destroy(currentWayPoint);
                if (!isNPC && !toInteractableObject) 
                {
                    instantiateWaypoint(targetPosition);
                }
                

                List<Node> path = retracePath(startNode, targetNode);


                //If the path is going to interactable object, we stop at the node just before it.
                if (toInteractableObject && path.Count >= 2) path.RemoveAt(path.Count - 1);
                
                grid.path = path;
                mover.updatePath(path);
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

    List<Node> retracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        return path;
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
                    findPath(targetNode.worldPosition, false);
                }
                
            }
            

            if (!mover.getIsAtEndOfPath())
            {
                yield return null;
            }

        }
    }

    public void startRoam()
    {
        StopAllCoroutines();
        StartCoroutine(roamRutine);
        numberOfRoutinesRunning++;
    }

    public void stopRoam()
    {
        //StopCoroutine(roamRutine);
        StopAllCoroutines();
        numberOfRoutinesRunning=0;
    }

}
