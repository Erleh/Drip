using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    bool _isAlive;

    void Awake()
    {
        _isAlive = true;
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public void OnAttacked()
    {
        _isAlive = false;
        GetComponent<Animator>().SetTrigger("die");
    }
}
