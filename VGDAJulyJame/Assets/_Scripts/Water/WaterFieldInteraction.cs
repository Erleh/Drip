using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFieldInteraction : MonoBehaviour
{
    //[SerializeField]
    //private ParticleSystem playerParticleSystem;
    //[SerializeField]
    //private ParticleSystem monsterParticleSystem;
    [SerializeField]
    private EnemyStatus monsterStatus;
    [SerializeField]
    private List<Collider2D> affected;

    private void OnDisable()
    {
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
        affected.Remove(col);
        WaterWalkingManager.CreateWaterResponseRequest(col.gameObject, WaterReaction, false);
    }

    void WaterReaction(bool enemy, bool player, bool onWater, Collider2D col)
    {
        if (enemy)
        {
            if (onWater)
                WaterWalkingManager.ActivateEnemyWaterParticles();
                //col.GetComponentInChildren<ParticleSystem>().Play();
            else if (!onWater)
                WaterWalkingManager.DisableEnemyWaterParticles();
                //col.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (player)
        {
            if (onWater)
                WaterWalkingManager.ActivatePlayerWaterParticles();
            //col.GetComponent<ParticleSystem>().Play();
            else if (!onWater)
                WaterWalkingManager.DisablePlayerWaterParticles();
                //col.GetComponent<ParticleSystem>().Stop();
        }
    }
}
