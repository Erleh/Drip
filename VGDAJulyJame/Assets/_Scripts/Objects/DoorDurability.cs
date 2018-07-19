using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorDurability : MonoBehaviour, BreakableBase
{
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
        Destroy(door);
    }

    // while the enemy interacts with the door, 
    public void OnInteraction(bool aggressive)
    {
        if(aggressive)
        {
            if (currentDurability > 0)
            {
                currentDurability -= durabilityLoss;
            }
            else if (currentDurability == 0)
            {
                OnDestroyed();
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            OnInteraction(true);
        }
    }
}
