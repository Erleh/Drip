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
            waterManager.WaterWalkingSuccess(true, false);
        }
        if(self.CompareTag("Player"))
        {
            waterManager.WaterWalkingSuccess(false, true);
        }
    }
}
