using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFieldInteraction : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem monsterParticleSystem;

    void OnTriggerEnter2D(Collider2D col)
    {
        WaterWalkingManager.CreateWaterResponseRequest(col.gameObject, WaterReaction);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        WaterWalkingManager.CreateWaterResponseRequest(col.gameObject, WaterReaction);
    }

    void WaterReaction(bool enemy, bool player)
    {
        if (enemy)
        {
            //print("enemy particle start");
            if (monsterParticleSystem.isEmitting)
            {
                //print("stopping...");
                monsterParticleSystem.Stop();
            }
            else
            {
                //print("starting....");
                monsterParticleSystem.Play();
            }
        }

        if (player)
        {

        }
    }
}
