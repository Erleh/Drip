using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfind : MonoBehaviour
{
    MapGrid grid;

    [SerializeField]
    RequestPath requestPath;

    void Awake()
    {
        grid = GetComponent<MapGrid>();
    }

    public void StartFindPath(Vector2 startPos, Vector2 endPos)
    {
        StartCoroutine(FindPath(startPos, endPos));
    }

    // finds a path within the grid from start node to end node
    //previous iteration----
    //public void FindPath(Vector2 startPos, Vector2 endPos)
    //-------
    IEnumerator FindPath(Vector2 startPos, Vector2 endPos)
    {
        ItemHeap<Node> openSet = new ItemHeap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        Vector2[] waypoints = new Vector2[0];

        Node startNode = grid.WorldToNodePoint(startPos);
        Node targetNode = grid.WorldToNodePoint(endPos);

        openSet.Add(startNode);

        bool foundPath = false;
        
        // if we want the monster to kill the player instantly
        if(Vector2.Distance(startPos, endPos) > 0)
        {
            if (startNode.passable && targetNode.passable)
            {
                // while nodes that have not been explored exist
                while (openSet.Count > 0)
                {
                    // retrieves first item from heap via pop
                    Node currentNode = openSet.Pop();

                    closedSet.Add(currentNode);

                    // if the target node has been found, loop ends and call to follow the found path
                    if (currentNode == targetNode)
                    {
                        foundPath = true;
                        break;
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

                            if (!openSet.Contains(n))
                            {
                                openSet.Add(n);
                            }

                            openSet.UpdateItem(n);
                        }
                    }
                }
            }
            
            yield return null;
            if(foundPath)
            {
                waypoints = CreatePath(startNode, targetNode);
            }
            
            requestPath.FinishedProcessingPath(waypoints, foundPath);
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

    // returns path of vectors of only positions where the node has changed location/direction
    // this way, it avoids repeats and saves on memory
    Vector2[] SimplifyPath(List<Node> path)
    {
        List<Vector2> newPath = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 newDirection = new Vector2(path[i - 1].gridX - path[i].gridX,
                                               path[i - 1].gridY - path[i].gridY);

            if (newDirection != directionOld && i != path.Count - 1)
            {
                newPath.Add(path[i].worldPos);
            }
            else if(i == path.Count - 1)
            {
                newPath.Add(path[i].worldPos);
            }
        }

        return newPath.ToArray();
    }

    // follows path of nodes from start node to end node, using set parent node of the end node
    //void FollowPath(Node start, Node end)
    Vector2[] CreatePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while(currentNode != start)
        {
            path.Add(currentNode);

            currentNode = currentNode.parentNode;
        }

        if(currentNode == start)
        {
            path.Add(currentNode);
        }

        Vector2[] waypoints = SimplifyPath(path);

        Array.Reverse(waypoints);

        // to reverse the list of nodes
        //path.Reverse();
        //grid.path = path;

        return waypoints;
    }
}
