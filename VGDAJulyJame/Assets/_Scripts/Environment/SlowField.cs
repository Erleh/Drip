using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SlowField - Increases linear drag coefficient for entering/exiting rigidbodies
public class SlowField : MonoBehaviour {
    
    //Set drag coefficient in inspector
    //Note: realistic values range 40-100
    [SerializeField]
    private float dragCoeff;

    //Set entering drag coefficient of rigidbody to number defined in inspector
    public void OnTriggerEnter2D(Collider2D col)
    {
        //grabbing rigidbody component of object...
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D affectedrb2d = col.gameObject.GetComponent<Rigidbody2D>();
            affectedrb2d.drag = dragCoeff;
        }
        Debug.Log("Object entered field: " + col.gameObject);
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D affectedrb2d = col.gameObject.GetComponent<Rigidbody2D>();
            //reset value to its default value
            affectedrb2d.drag = 0;
        }
        Debug.Log("Object exited field: " + col.gameObject);
    }
}
