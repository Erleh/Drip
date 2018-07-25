using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFieldInteraction : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem monsterParticleSystem;
    [SerializeField]
    private EnemyStatus monsterStatus;
    [SerializeField]
    private List<Collider2D> affected;

    private void OnDisable()
    {
        //print("Disabled");
        foreach(Collider2D col in affected)
        {
            WaterWalkingManager.CreateWaterResponseRequest(col.gameObject, WaterReaction, false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        affected.Add(col);
        
        WaterWalkingManager.CreateWaterResponseRequest(col.gameObject, WaterReaction, true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //print("exit");
        affected.Remove(col);
        WaterWalkingManager.CreateWaterResponseRequest(col.gameObject, WaterReaction, false);
    }

    void WaterReaction(bool enemy, bool player, bool onWater)
    {
        if (enemy)
        {
            if (onWater)
                monsterParticleSystem.Play();
            else if (!onWater)
                monsterParticleSystem.Stop();
        }

        if (player)
        {

        }
    }
}
