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
    private Animator pAnim;
    //public delegate void onDeathEvent();
    //public event onDeathEvent Died;

    void Awake()
    {
        pAnim = GetComponent<Animator>();
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
        pAnim.SetTrigger("die");
        AkSoundEngine.PostEvent("Player_Death", gameObject);
        AkSoundEngine.SetState("PlayerLife", "Dead");
    }
    private void Update()
    {
        if (IsAlive()) { AkSoundEngine.SetState("Moving", "Idle"); }
        if (Moving()) { AkSoundEngine.SetState("Moving", "Walking"); }
        if (Pushing()) { AkSoundEngine.SetState("Moving", "Pushing"); }
    }
    public void DTOne(){    pAnim.SetTrigger("dt1");    }
    public void DTTwo(){    pAnim.SetTrigger("dt2");    }
}
