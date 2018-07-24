using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, EnemyMovementBase
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private Transform target;

    [Header("Set fields below")]
    [SerializeField]
    Transform enemyTrans;
    [SerializeField]
    Rigidbody2D enemyRB;
    [SerializeField]
    GameObject triggerDetectionBox;
    [SerializeField]
    MapGrid mapGrid;
    [SerializeField]
    ParticleSystem waterTrail;

    // reps the index of the position on the path this object is on
    int posIndex;
    // path to target
    private Vector2[] path;

    bool hunting;

    void FixedUpdate()
    {
        enemyRB.velocity = Vector2.zero;
    }

    // will most likely not need this
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.CompareTo("Player") == 0 && !hunting)
        {
            if (Vector2.Distance(transform.position, col.transform.position) > 1)
            {
                hunting = true;
                target = col.transform;
                RequestPath.CreatePathRequest(enemyTrans.position, target.position, OnPathFound);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.CompareTo("Player") == 0 && !hunting)
        {
            if (Vector2.Distance(transform.position, col.transform.position) > 1)
            {
                hunting = true;
                target = col.transform;
                RequestPath.CreatePathRequest(enemyTrans.position, target.position, OnPathFound);
            }
        }
    }

    public IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[0];
        Vector2 currPos;

        while (true)// && hasWaypoint)
        {
            currPos = new Vector2(enemyTrans.position.x, enemyTrans.position.y);
            float oldPosX = currPos.x;
            float oldPosY = currPos.y;
            
            if (mapGrid.WorldToNodePoint(currPos) == mapGrid.WorldToNodePoint(currentWaypoint))
            {
                posIndex++;

                if (posIndex >= path.Length)
                {
                    posIndex = 0;

                    yield return new WaitForSeconds(1);

                    hunting = false;
                    triggerDetectionBox.SetActive(false);
                    triggerDetectionBox.SetActive(true);
                    
                    // stop coroutine
                    yield break;
                }
                currentWaypoint = path[posIndex];
            }

            enemyRB.AddForce(Vector3.Normalize(currentWaypoint - currPos) * speed);

            //Vector2 newPos = new Vector2(enemyTrans.position.x, enemyTrans.position.y);
            
            // if stuck
            if (ApproxVals(currPos.x, oldPosX, .01f) && ApproxVals(currPos.y, oldPosY, .01f))
            {
                print("stuck");
                yield return new WaitForSeconds(1);

                hunting = false;
                triggerDetectionBox.SetActive(false);
                triggerDetectionBox.SetActive(true);

                // stop coroutine
                yield break;
            }
            //enemyTrans.position = Vector2.MoveTowards(enemyTrans.position, currentWaypoint, speed);

            yield return null;
        }
    }

    // approx between 2 bools, return whether difference is greater that tolerated level
    public bool ApproxVals(float a, float b, float tollerance)
    {
        if (Mathf.Abs(b - a) < tollerance)
        {
            return false;
        }
        else
            return true;
    }

    public void OnPathFound(Vector2[] _path, bool pathFound)
    {
        if(pathFound)
        {
            path = _path;
            // stop whatever instance of followpath exists already and start new
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            hunting = false;

            triggerDetectionBox.SetActive(false);
            triggerDetectionBox.SetActive(true);
            
            StopCoroutine("FollowPath");
        }
    }
}
