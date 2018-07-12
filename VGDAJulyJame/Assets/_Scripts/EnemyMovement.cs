using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, EnemyMovementBase
{
    [SerializeField]
    Transform enemyTrans;
    [SerializeField]
    Rigidbody2D enemyRB;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private Transform target;
    [SerializeField]
    GameObject triggerDetectionBox;

    // reps the index of the position on the path this object is on
    int posIndex;
    // path to target
    private Vector2[] path;

    bool hunting;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.CompareTo("Player") == 0 && !hunting)
        {
            //print("hit");
            hunting = true;
            target = col.transform;
            RequestPath.CreatePathRequest(enemyTrans.position, target.position, OnPathFound);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag.CompareTo("Player") == 0)
        {
            //hunt = false;
        }
    }

    public IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[0];
        //print("trying to follow path...");
        
        while (true)
        {
            Vector2 currPos = new Vector2(enemyTrans.position.x, enemyTrans.position.y);
            //print("trying to get to waypoint...");
            if (currPos == currentWaypoint)
            {
                posIndex++;
                //print("posIndex = " + posIndex);
                //print("next destination = " + path[posIndex]);

                if (posIndex >= path.Length)
                {
                    //print("break");
                    posIndex = 0;

                    //if (hunt)
                    //{
                    //    RequestPath.CreatePathRequest(enemyTrans.position, target.position, OnPathFound);
                    //}
                    hunting = false;

                    yield return new WaitForSeconds(1);
                    triggerDetectionBox.SetActive(false);
                    triggerDetectionBox.SetActive(true);
                    
                    // stop coroutine
                    yield break;
                }
                currentWaypoint = path[posIndex];
            }

            //enemyRB.AddForce((currentWaypoint - currPos) * speed);
            enemyTrans.position = Vector2.MoveTowards(enemyTrans.position, currentWaypoint, speed);

            yield return null;
        }
    }

    public void OnPathFound(Vector2[] _path, bool pathFound)
    {
        if(pathFound)
        {
            //print("path found");

            path = _path;
            // stop whatever instance of followpath exists already and start new
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
}
