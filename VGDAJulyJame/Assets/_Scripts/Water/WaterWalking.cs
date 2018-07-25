using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWalking : MonoBehaviour
{
    [SerializeField]
    WaterWalkingManager waterManager;

    public void StartWaterAffect(GameObject self, bool onWater)
    {
        if(self.CompareTag("Enemy"))
        {
            //print("enemy found");
            waterManager.FinishedProcess(true, false, onWater, self.GetComponent<Collider2D>());
        }
        if(self.CompareTag("Player"))
        {
            //print("player found");
            waterManager.FinishedProcess(false, true, onWater, self.GetComponent<Collider2D>());
        }
    }
}
