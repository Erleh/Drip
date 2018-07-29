using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorDurability : MonoBehaviour, BreakableBase
{
    [SerializeField]
    private ParticleSystem doorSplinters;

    [SerializeField]
    private GameObject door;

    [SerializeField]
    private float maxDurability = 100;

    private float currentDurability;

    [SerializeField]
    private float durabilityLoss = .5f;

    void Start()
    {
        currentDurability = maxDurability;
    }

    public void OnDestroyed()
    {
        doorSplinters.Play();
        Destroy(door);
    }

    // while the enemy interacts with the door, 
    public void OnInteraction(bool aggressive, Collider2D col)
    {
        if(aggressive)
        {
            if (currentDurability > 0)
            {
                currentDurability -= durabilityLoss;
            }
            else if (currentDurability == 0)
            {
                col.isTrigger = true;
                OnDestroyed();
            }
        }
    }

    //void OnCollisionStay2D(Collision2D col)
    void OnTriggerStay2D(Collider2D col)
    {
        print("woke");
        if(col.gameObject.CompareTag("Enemy"))
        {
            OnInteraction(true, col);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {

    }
}
