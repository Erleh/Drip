using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // will use this class for setting values active on the monster

    static bool onWater = false;
    public bool IsOnWater { get { return onWater; } set { onWater = value; }}
}
