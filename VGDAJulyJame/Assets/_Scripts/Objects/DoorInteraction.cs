using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private HingeJoint2D doorHinge;
    private JointMotor2D doorMotor;

    [SerializeField]
    private GameObject door;

    void Start()
    {
        doorHinge = door.GetComponent<HingeJoint2D>();
        doorMotor = doorHinge.motor;
    }

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
}
