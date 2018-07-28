using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerInteraction : MonoBehaviour
{
    [SerializeField]
    private Animator sprinklerAnim;

    [SerializeField]
    private GameObject SlowField;

    [SerializeField]
    private float sprinklerDuration;

    private Coroutine SprinklerCD;

    private void Awake()
    {
        SprinklerCD = null;
        AkSoundEngine.SetState("SprinklerStatus", "Off");
    }

    void GetInteraction()
    {
        //If the sprinkler is not on cooldown and the player interacts with the valve
        if (Input.GetButtonDown("Interact") && !IsActive())
        {
            //Debug.Log("Attempting Interaction...");
            //Turn the sprinkler on for the determined amount of time
            sprinklerAnim.SetBool("SprinklerActive", true);
            SprinklerCD = StartCoroutine(SprinklerCooldown());
            SlowField.SetActive(true);
            AkSoundEngine.PostEvent("Sprinkler_Start", gameObject);
            Debug.Log("Yay!");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Registered Player.");
            GetInteraction();
        }
    }

    IEnumerator SprinklerCooldown()
    {
        //Debug.Log("Begin Cooldown.");
        yield return new WaitForSeconds(sprinklerDuration);
        //Debug.Log("End Cooldown.");
        sprinklerAnim.SetBool("SprinklerActive", false);
        SlowField.SetActive(false);
        SprinklerCD = null;
        AkSoundEngine.SetState("SprinklerStatus", "Off");
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
