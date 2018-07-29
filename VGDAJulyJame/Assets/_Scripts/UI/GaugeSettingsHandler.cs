using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GaugeSettingsHandler : MonoBehaviour {

    /// <summary>
    /// It is worth noting that there is a lot of duplicate code here
    /// I'll fix it when we go to refactor after the deadline
    /// Also eric, I'm sorry this looks so disgusting
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

    //Check for Plus Hold
    private bool checkHeldP;
    //Check for Minus Hold
    private bool checkHeldM;
    //Saved reference to prevent player from overlapping coroutine calls
    private Coroutine holdingCo;
    // currVal needs to be set before Start so that the sound handler may access it properly
    private void Awake()
    {
        currVal = startingVal;
    }
    void Start () {
        UINeedle.transform.rotation = Quaternion.Euler(0,0, startingAngle);
        currentAngle = UINeedle.transform.eulerAngles;
        rotatingQueue = new CoroutineQueue(this);
        rotatingQueue.StartLoop();
        meterVal.text = currVal + "";
    }
    private void FixedUpdate()
    {
        if (checkHeldM)
        {
            DecreaseValue();
        }
        if(checkHeldP)
        {
            IncreaseValue();
        }
    }
    public void ChangeHeldMD() { checkHeldM = true; }
    public void ChangeHeldMU() { checkHeldM = false; }
    public void ChangeHeldPD() { checkHeldP = true; }
    public void ChangeHeldPU() { checkHeldP = false; }

    public float GetValue() { return currVal; }
    public void IncreaseValue()
    {
        if (holdingCo == null)
        {
            if (currVal < maxVal)
            {
                if (checkHeldP)
                    holdingCo = StartCoroutine(IncreaseNeedle());
                else
                {
                    rotatingQueue.EnqueueAction(IncreaseNeedle());
                }
                currVal = Mathf.Clamp(currVal + valIncrements, minVal, maxVal);
                meterVal.text = currVal + "";
            }
        }
    }
    public void DecreaseValue()
    {
        if (holdingCo == null)
        {
            if (currVal > minVal)
            {
                if (checkHeldM)
                    holdingCo = StartCoroutine(DecreaseNeedle());
                else
                {
                    rotatingQueue.EnqueueAction(DecreaseNeedle());
                }
                currVal = Mathf.Clamp(currVal - valIncrements, minVal, maxVal);
                meterVal.text = currVal + "";
            }
        }
    }
    IEnumerator DecreaseNeedle()
    {
        Vector3 targetAngle = new Vector3(0, 0, currentAngle.z + eulerIncrements);
        //Debug.Log("Target Angle: " + targetAngle);
        while (currentAngle.z < targetAngle.z)
        {
            //Debug.Log(currentAngle.Equals(targetAngle));
            currentAngle = Vector3.MoveTowards(currentAngle, targetAngle, turnSpeed * Time.deltaTime);
            UINeedle.transform.eulerAngles = currentAngle;
            yield return null;
        }
        if (checkHeldM)
        {
            yield return new WaitForSeconds(0.01f);
            holdingCo = null;
        }
        else
        {
            yield return null;
            holdingCo = null;
        }
    }
    IEnumerator IncreaseNeedle()
    {
        Vector3 targetAngle = new Vector3(0, 0, currentAngle.z - eulerIncrements);
        //Debug.Log("Target Angle: " + targetAngle);
        while (currentAngle.z > targetAngle.z)
        {
            //Debug.Log(currentAngle.Equals(targetAngle));
            currentAngle = Vector3.MoveTowards(currentAngle, targetAngle, turnSpeed * Time.deltaTime);
            UINeedle.transform.eulerAngles = currentAngle;
            yield return null;
        }
        if (checkHeldP)
        {
            yield return new WaitForSeconds(0.01f);
            holdingCo = null;
        }
        else
        {
            yield return null;
            holdingCo = null;
        }
    }
}
