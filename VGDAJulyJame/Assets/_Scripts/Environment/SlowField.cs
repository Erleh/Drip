using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SlowField - Increases linear drag coefficient for entering/exiting rigidbodies
public class SlowField : MonoBehaviour {
    
    //Set drag coefficient in inspector
    //Note: realistic values range 40-100
    [SerializeField]
    private float dragCoeff;

    //Maintain list of rigidbodies currently affected by the puddle
    [SerializeField]
    private List<Rigidbody2D> affectedRigidbodies;

    [SerializeField]
    private float pushableDrag;
    [SerializeField]
    private float characterDrag;

    [SerializeField]
    private Dictionary<GameObject, float> defaults = new Dictionary<GameObject, float>();

    //Set entering drag coefficient of rigidbody to number defined in inspector, or add pushable amount if it's a pushable object
    public void OnTriggerEnter2D(Collider2D col){   IncreaseDrag(col);  }

    //if it's not a pushable object, reset its drag coeff
    public void OnTriggerExit2D(Collider2D col){    ResetDragSingle(col);  }

    private void IncreaseDrag(Collider2D col)
    {
        //grabbing rigidbody component of object...
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D affectedrb2d = col.gameObject.GetComponent<Rigidbody2D>();
            defaults.Add(col.gameObject, affectedrb2d.drag);
            if (col.gameObject.CompareTag("Pushable"))
                affectedrb2d.drag += pushableDrag;
            else
                affectedrb2d.drag += characterDrag;
            affectedRigidbodies.Add(affectedrb2d);
        }
    }

    private void ResetDragSingle(Collider2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D affectedrb2d = col.gameObject.GetComponent<Rigidbody2D>();
            affectedrb2d.drag = defaults[col.gameObject];
            defaults.Remove(col.gameObject);
            affectedRigidbodies.Remove(affectedrb2d);
        }
    }
    private void ResetAllDrag()
    {
        foreach (Rigidbody2D affected in affectedRigidbodies)
        {
            if (affected != null)
            {
                affected.drag = defaults[affected.gameObject];
            }
        }
        defaults.Clear();
        affectedRigidbodies.Clear();
    }
    public void OnDisable()
    {
        //If the puddle disappears with objects in it, return all objects inside to normal state
        ResetAllDrag();
    }
}
