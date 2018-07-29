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

    public bool IsAlive() { return _isAlive; }
    public bool Moving() { return pMovement.GetMoving(); }
    public bool Pushing() { return pMovement.GetPushing(); }
    public void OnAttacked()
    {
        onDeath.Invoke();
    }
    public void Die()
    {
        _isAlive = false;
        GetComponent<Animator>().SetTrigger("die");
        AkSoundEngine.PostEvent("Player_Death", gameObject);
        AkSoundEngine.SetState("PlayerLife", "Dead");
    }
    private void Update()
    {
        if (IsAlive()) { AkSoundEngine.SetState("Moving", "Idle"); }
        if (Moving()) { AkSoundEngine.SetState("Moving", "Walking"); }
        if (Pushing()) { AkSoundEngine.SetState("Moving", "Pushing"); }


    }
}
