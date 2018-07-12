using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RequestPath : MonoBehaviour
{
    // Queue for PathRequests
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    // self reference
    static RequestPath instance;

    [SerializeField]
    private Pathfind pathfind;

    // marks when next path in queue is being processed so that the next
    // request needs to wait
    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
    }

    // Function to be called by enemies or objects requiring pathfinding
    // the Action is something to be performed once path has been found -- indicated by the bool
    // if the path has been found then act on the function within the action -- path rep by the Vector2[]
    public static void CreatePathRequest(Vector2 startPos, Vector2 endPos, Action<Vector2[], bool> doAfter)
    {
        //print("trying to make newRequest...");
        PathRequest newRequest = new PathRequest(startPos, endPos, doAfter);
        //print("trying to enqueue...");
        instance.pathRequestQueue.Enqueue(newRequest);
        //print("trying next request...");
        instance.TryNextRequest();
    }

    // if not currently processing a path, and the queue has a request in queue
    // begin processing and call method to find a path
    void TryNextRequest()
    {
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfind.StartFindPath(currentPathRequest.startPos, currentPathRequest.endPos);
        }
    }

    // call when path is found
    public void FinishedProcessingPath(Vector2[] path, bool success)
    {
        currentPathRequest.doAfter(path, success);
        isProcessingPath = false;
        TryNextRequest();
    }

    // information needed per request for path
    struct PathRequest
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public Action<Vector2[], bool> doAfter;

        public PathRequest(Vector2 _startPos, Vector2 _endPos, Action<Vector2[], bool> _doAfter)
        {
            startPos = _startPos;
            endPos = _endPos;
            doAfter = _doAfter;
        }
    }
}
