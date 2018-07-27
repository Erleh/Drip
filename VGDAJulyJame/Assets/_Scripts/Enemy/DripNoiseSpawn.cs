﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripNoiseSpawn : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 3f;

    void Start()
    {
        //insert play sound here
        AkSoundEngine.PostEvent("Monster_Drip", gameObject);
        StartCoroutine(TerminateDelay());
    }

    public IEnumerator TerminateDelay()
    {
        //print("test");
        yield return new WaitForSeconds(lifeTime);

        DestroySelf();

        yield break;
    }

    void DestroySelf()
    {
        //print("destroy");
        Destroy(this.gameObject);
    }
}
