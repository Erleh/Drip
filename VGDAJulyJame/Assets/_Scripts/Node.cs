using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public Node parentNode;

    public bool passable;
    public Vector2 worldPos;
    public int gridX;
    public int gridY;

    // distance from start node
    public int gCost;
    // distance from end node
    public int hCost;

    int heapIndex;

    public Node(bool _passable, Vector2 position, int _gridX, int _gridY)
    {
        passable = _passable;
        worldPos = position;
        gridX = _gridX;
        gridY = _gridY;
    }

    // sum gCost and hCost
    public int fCost { get { return gCost + hCost; } }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }

        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other)
    {
        int compareVal = fCost.CompareTo(other.fCost);

        // for our purpose, value that is lower, is of a greater priority
        // so return 1 if value is < other val
        // and return -1 if value is > other val
        if (compareVal == 0)
        {
            compareVal = hCost.CompareTo(other.hCost);
        }
        return -compareVal;
    }
}
