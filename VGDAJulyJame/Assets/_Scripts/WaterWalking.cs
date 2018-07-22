using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWalking : MonoBehaviour
{
    [SerializeField]
    WaterWalkingManager waterManager;

    public void StartWaterAffect(GameObject self)
    {
        if(self.CompareTag("Enemy"))
        {
            print("enemy found");
            waterManager.FinishedProcess(true, false, true);
        }
        if(self.CompareTag("Player"))
        {
            print("player found");
            waterManager.FinishedProcess(false, true, true);
        }
    }
}
