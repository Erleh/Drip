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
    public void OnInteraction(bool aggressive, Collision2D col)
    {
        if(aggressive)
        {
            if (currentDurability > 0)
            {
                currentDurability -= durabilityLoss;
            }
            else if (currentDurability == 0)
            {
                col.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
                OnDestroyed();
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            OnInteraction(true, col);
        }
    }
}
