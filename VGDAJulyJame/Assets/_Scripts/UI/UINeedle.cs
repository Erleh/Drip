using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UINeedle : MonoBehaviour {

    [SerializeField]
    private float minAngle;
    [SerializeField]
    private float maxAngle;
    [SerializeField]
    private float startingAngle;
    [SerializeField]
    private Vector3 currentAngle;
    [SerializeField]
    private float eulerIncrements;
    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float startingVal;
    [SerializeField]
    private float currVal;
    private float minVal;
    private float maxVal;
    [SerializeField]
    private float valIncrements;
    private CoroutineQueue rotatingQueue;

    [SerializeField]
    private float lerpMargin;
    // Use this for initialization
    void Start () {
        transform.rotation = Quaternion.Euler(0,0, startingAngle);
        currentAngle = transform.eulerAngles;
        rotatingQueue = new CoroutineQueue(this);
        rotatingQueue.StartLoop();
        minVal = 0;
        maxVal = 100;
        currVal = startingVal;
        Debug.Log(transform.localEulerAngles);
    }
	
    public void IncreaseValue()
    {
        rotatingQueue.EnqueueAction(IncreaseNeedle());
        currVal = Mathf.Clamp(currVal + valIncrements, minVal, maxVal);
    }
    public void DecreaseValue()
    {
        rotatingQueue.EnqueueAction(DecreaseNeedle());
        currVal = Mathf.Clamp(currVal - valIncrements, minVal, maxVal);
    }
    IEnumerator IncreaseNeedle()
    {
        if (transform.localEulerAngles.z < maxAngle)
        {
            Vector3 targetAngle = new Vector3(0, 0, currentAngle.z + eulerIncrements);
            //Debug.Log("Target Angle: " + targetAngle);
            Vector3 rotStartAngle = currentAngle;
            while (currentAngle.z < targetAngle.z)
            {
                //Debug.Log(currentAngle.Equals(targetAngle));
                currentAngle = Vector3.MoveTowards(currentAngle, targetAngle, turnSpeed * Time.deltaTime);
                transform.eulerAngles = currentAngle;
                yield return null;
            }
        }
        Debug.Log("Finished.");
        yield return null;
    }
    IEnumerator DecreaseNeedle()
    {
        if (transform.localEulerAngles.z > minAngle)
        {
            Vector3 targetAngle = new Vector3(0, 0, currentAngle.z - eulerIncrements);
            //Debug.Log("Target Angle: " + targetAngle);
            while (currentAngle.z > targetAngle.z)
            {
                //Debug.Log(currentAngle.Equals(targetAngle));
                currentAngle = Vector3.MoveTowards(currentAngle, targetAngle, turnSpeed * Time.deltaTime);
                transform.eulerAngles = currentAngle;
                yield return null;
            }
        }
        Debug.Log("Finished.");
        yield return null;
    }
}
