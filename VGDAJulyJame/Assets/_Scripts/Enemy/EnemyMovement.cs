using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, EnemyMovementBase
{
    [SerializeField] GameObject dripObject;

    [SerializeField] private float speed = 5;
    [SerializeField] private Transform target;

    [Header("Set fields below")]
    [SerializeField] Transform enemyTrans;
    [SerializeField] Rigidbody2D enemyRB;
    [SerializeField] GameObject triggerDetectionBox;
    [SerializeField] MapGrid mapGrid;
    [SerializeField] ParticleSystem waterTrail;
    [SerializeField] private float maxPathSearchTime;
    
    private float distFromPlayer = 0;
    private Coroutine timer;

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
            distFromPlayer = Vector2.Distance(transform.position, col.transform.position);
            if (distFromPlayer > 1)
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
            distFromPlayer = Vector2.Distance(transform.position, col.transform.position);
            if (distFromPlayer > 1)
            {
                hunting = true;
                target = col.transform;
                RequestPath.CreatePathRequest(enemyTrans.position, target.position, OnPathFound);
            }
        }
    }

    public float GetDistanceFromPlayer()
    {
        return distFromPlayer;
    }

    public IEnumerator PathTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        timer = null;
    }
    
    public IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[0];
        Vector2 currPos;

        timer = StartCoroutine(PathTimer(5));

        while (timer != null)
        {
            currPos = new Vector2(enemyTrans.position.x, enemyTrans.position.y);
            var oldPosX = currPos.x;
            var oldPosY = currPos.y;
            
            if (mapGrid.WorldToNodePoint(currPos) == mapGrid.WorldToNodePoint(currentWaypoint))
            {
                posIndex++;
                
                if (posIndex >= path.Length)
                {
                    Instantiate(dripObject, enemyTrans.position, enemyTrans.rotation);
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
            
            // if stuck
            if (ApproxVals(currPos.x, oldPosX, .01f) && ApproxVals(currPos.y, oldPosY, .01f))
            {
                yield return new WaitForSeconds(1);

                hunting = false;
                triggerDetectionBox.SetActive(false);
                triggerDetectionBox.SetActive(true);

                // stop coroutine
                yield break;
            }

            yield return null;
        }

        if (timer == null)
        {
            hunting = false;
            triggerDetectionBox.SetActive(false);
            triggerDetectionBox.SetActive(true);

            yield break;
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
