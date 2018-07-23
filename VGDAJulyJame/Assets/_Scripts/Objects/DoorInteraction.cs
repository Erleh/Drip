using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Set fields bellow")]
    [SerializeField]
    private BoxCollider2D doorCollider;

    private bool open;

    void Awake()
    {
        open = false;
    }

    void OpenDoor()
    {
        doorCollider.enabled = false;
    }

    void CloseDoor()
    {
        doorCollider.enabled = true;
    }

    // if button to interact is pressed, trigger interaction with open and close door
    // when animations are in, action will be linked to points within the animation
    void GetInteraction()
    {
        if (Input.GetButtonDown("Interact"))
        {
            //print("hit");
            if (!open)
            {
                OpenDoor();
                open = true;
            }
            else
            {
                CloseDoor();
                open = false;
            }
        }
    }

    //animation based door
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GetInteraction();
        }
        if(col.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    /* works when with physics based door
    
    private HingeJoint2D doorHinge;
    private JointMotor2D doorMotor;

    [SerializeField]
    private GameObject door;

    void Start()
    {
        doorHinge = door.GetComponent<HingeJoint2D>();
        doorMotor = doorHinge.motor;
    }

    // trigger to sense interaction with door via "E" key
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorMotor.motorSpeed = -1 * doorMotor.motorSpeed;

                doorHinge.motor = doorMotor;
            }
        }
    }
    */
}
