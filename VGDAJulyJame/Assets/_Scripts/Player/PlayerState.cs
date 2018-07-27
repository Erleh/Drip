using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    PlayerMovement pMovement;
    bool _isAlive;

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
        _isAlive = false;
        GetComponent<Animator>().SetTrigger("die");
    }
}
