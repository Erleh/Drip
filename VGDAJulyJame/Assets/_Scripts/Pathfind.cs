using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour
{
    // temp, represents object trying to reach other object
    public Transform seeker, target;

    MapGrid grid;

    void Awake()
    {
        grid = GetComponent<MapGrid>();
    }

    // temp, updates path from seeker to target on update
    void Update()
    {
        FindPath(seeker.position, target.position);
    }

    // finds a path within the grid from start node to end node
    void FindPath(Vector2 startPos, Vector2 endPos)
    {
        ItemHeap<Node> openSet = new ItemHeap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        Node startNode = grid.WorldToNodePoint(startPos);
        Node targetNode = grid.WorldToNodePoint(endPos);

        openSet.Add(startNode);
        print(openSet.Count);
        // while nodes that have not been explored exist
        while (openSet.Count > 0)
        {
            // retrieves first item from heap via pop
            Node currentNode = openSet.Pop();
            
            closedSet.Add(currentNode);

            // if the target node has been found, loop ends and call to follow the found path
            if (currentNode == targetNode)
            {
                //print("target found");
                //print("start location = " + startPos.x + "," + startPos.y);
                //print("target location = " + targetNode.gridX + "," + targetNode.gridY);

                FollowPath(startNode, targetNode);
                return;
            }

            // for each neighboring node near by current node
            foreach (Node n in grid.GetNeighbourNodes(currentNode))
            {
                if (!n.passable || closedSet.Contains(n))
                {
                    continue;
                }

                int distanceFromNeighbour = currentNode.gCost + GetDistance(currentNode, n);

                if (distanceFromNeighbour < n.gCost || !openSet.Contains(n))
                {
                    n.gCost = distanceFromNeighbour;
                    n.hCost = GetDistance(n, targetNode);

                    n.parentNode = currentNode;

                    if(!openSet.Contains(n))
                    {
                        openSet.Add(n);
                    }

                    openSet.UpdateItem(n);
                }
            }
        }
    }

    // find the distance between 2 nodes
    int GetDistance(Node x, Node y)
    {
        int xDistance = Mathf.Abs(y.gridX - x.gridX);
        int yDistance = Mathf.Abs(y.gridY - x.gridY);

        int totalDistance;
        if (xDistance > yDistance)
            totalDistance = 14 * yDistance + 10 * (xDistance - yDistance);
        else
            totalDistance = 14 * xDistance + 10 * (yDistance - xDistance);

        return totalDistance;
    }

    // follows path of nodes from start node to end node, using set parent node of the end node
    void FollowPath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while(currentNode != start)
        {
            path.Add(currentNode);

            currentNode = currentNode.parentNode;
        }

        path.Reverse();
        grid.path = path;
    }
}
