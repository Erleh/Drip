using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GaugeSettingsHandler : MonoBehaviour {

    /// <summary>
    /// It is worth noting that there is a lot of duplicate code here
    /// I'll fix it when we go to refactor after the deadline
    /// -Justin Gonzalez
    /// </summary>
    
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

    [SerializeField]
    private float minVal;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float valIncrements;
    private CoroutineQueue rotatingQueue;

    [SerializeField]
    private Text meterVal;
    [SerializeField]
    private GameObject UINeedle;

    // Use this for initialization
    void Start () {
        UINeedle.transform.rotation = Quaternion.Euler(0,0, startingAngle);
        currentAngle = UINeedle.transform.eulerAngles;
        rotatingQueue = new CoroutineQueue(this);
        rotatingQueue.StartLoop();
        currVal = startingVal;
        Debug.Log(UINeedle.transform.localEulerAngles);
        meterVal.text = 0 + "";
    }
	
    public void IncreaseValue()
    {
        if (currVal < maxVal)
        {
            rotatingQueue.EnqueueAction(IncreaseNeedle());
            currVal = Mathf.Clamp(currVal + valIncrements, minVal, maxVal);
            meterVal.text = currVal + "";
        }
    }
    public void DecreaseValue()
    {
        if (currVal > minVal)
        {
            rotatingQueue.EnqueueAction(DecreaseNeedle());
            currVal = Mathf.Clamp(currVal - valIncrements, minVal, maxVal);
            meterVal.text = currVal + "";
        }
    }
    IEnumerator DecreaseNeedle()
    {
        Debug.Log(UINeedle.transform.eulerAngles.z);
        Vector3 targetAngle = new Vector3(0, 0, currentAngle.z + eulerIncrements);
        //Debug.Log("Target Angle: " + targetAngle);
        while (currentAngle.z < targetAngle.z)
        {
            //Debug.Log(currentAngle.Equals(targetAngle));
            currentAngle = Vector3.MoveTowards(currentAngle, targetAngle, turnSpeed * Time.deltaTime);
            UINeedle.transform.eulerAngles = currentAngle;
            yield return null;
        }
        Debug.Log("Finished.");
        yield return null;
    }
    IEnumerator IncreaseNeedle()
    {
        Debug.Log(UINeedle.transform.eulerAngles.z);
        Vector3 targetAngle = new Vector3(0, 0, currentAngle.z - eulerIncrements);
        //Debug.Log("Target Angle: " + targetAngle);
        while (currentAngle.z > targetAngle.z)
        {
            //Debug.Log(currentAngle.Equals(targetAngle));
            currentAngle = Vector3.MoveTowards(currentAngle, targetAngle, turnSpeed * Time.deltaTime);
            UINeedle.transform.eulerAngles = currentAngle;
            yield return null;
        }
        Debug.Log("Finished.");
        yield return null;
    }
}
