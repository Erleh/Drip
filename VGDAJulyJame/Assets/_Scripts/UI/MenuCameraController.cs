using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour {

    [SerializeField]
    private float xMovement;
    [SerializeField]
    private float camMoveSpeed;
    [SerializeField]
    private GameObject RotatingKnob;
    [SerializeField]
    private float lerpMargin;
    private Coroutine MovableCam;
    private Vector3 camMoveTarget;
    [SerializeField]
    private bool forward;


    private void Start()
    {
        DetermineTarget();
    }
    public void MoveCamera()
    {
        if (MovableCam == null)
        {
            DetermineTarget();
            MovableCam = StartCoroutine(MoveCo());
        }
    }
    private void DetermineTarget()
    {
        forward = !forward;
        if (forward)
            camMoveTarget = new Vector3(transform.position.x + xMovement, 0, transform.position.z);
        else
            camMoveTarget = new Vector3(transform.position.x - xMovement, 0, transform.position.z);
    }
    IEnumerator MoveCo()
    {
        if (forward)
        {
            while (transform.position.x < camMoveTarget.x - lerpMargin)
            {
                transform.position = Vector3.Lerp(this.transform.position, camMoveTarget, Time.deltaTime * camMoveSpeed);
                yield return null;
            }
        }
        else
        {
            while (transform.position.x > camMoveTarget.x + lerpMargin)
            {
                Debug.Log("Attempting to move backwards...");
                transform.position = Vector3.Lerp(this.transform.position, camMoveTarget, Time.deltaTime * camMoveSpeed);
                yield return null;
            }
        }
        Debug.Log("Finished Moving.");
        yield return null;
        MovableCam = null;
    }
}
