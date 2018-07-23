using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessObjectRotation : MonoBehaviour {

    [SerializeField]
    private float maxTimer;
    private float rotationSpeed;
    public void RotateObject(float rotSpd)
    {
        rotationSpeed = rotSpd;
        StartCoroutine(RotateObjectCo());
    }

    IEnumerator RotateObjectCo()
    {
        float timer = 0f;
        while (timer <= maxTimer)
        {
            transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime * 360f));
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
