using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerInteraction : MonoBehaviour {

    [SerializeField]
    private GameObject SlowField;

    [SerializeField]
    private float CDTimer;

    private Coroutine SprinklerCD;
    private void Awake()
    {
        SprinklerCD = null;
    }

    void GetInteraction()
    {
        //If the sprinkler is not on cooldown and the player interacts with the valve
        if (Input.GetButtonDown("Interact") && !IsActive())
        {
            Debug.Log("Attempting Interaction...");
            //Turn the sprinkler on for the determined amount of time
            SprinklerCD = StartCoroutine(SprinklerCooldown());
            SlowField.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Registered Player.");
            GetInteraction();
        }
    }
    IEnumerator SprinklerCooldown()
    {
        Debug.Log("Begin Cooldown.");
        yield return new WaitForSeconds(CDTimer);
        Debug.Log("End Cooldown.");
        SlowField.SetActive(false);
        SprinklerCD = null;
    }

    bool IsActive()
    {
        if (SprinklerCD == null)
        {
            return false;
        }
        else
            return true;
    }
}
