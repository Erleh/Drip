using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onDeath;


    private PlayerMovement pMovement;
    bool _isAlive;

    //public delegate void onDeathEvent();
    //public event onDeathEvent Died;

    void Awake()
    {
        _isAlive = true;
        pMovement = GetComponent<PlayerMovement>();
    }

    public bool IsAlive(){  return _isAlive;                }
    public bool Moving(){   return pMovement.GetMoving();   }
    public bool Pushing(){  return pMovement.GetPushing();  }
    public void OnAttacked()
    {
        Debug.Log("Player Was Attacked.");
        onDeath.Invoke();
        Debug.Log("Called onDeath functions.");
    }
    public void Die()
    {
        _isAlive = false;
        GetComponent<Animator>().SetTrigger("die");
        //AkSoundEngine.PostEvent("Player_Death");
        Debug.Log("Triggered player death.");
    }
    private void Update()
    {
        if (IsAlive()){AkSoundEngine.PostEvent("Player_Idle");}
        if (Moving()) { AkSoundEngine.PostEvent("Player_Walking"); }
        if (Pushing()) { AkSoundEngine.PostEvent("Player_Pushing"); }

    }
}
