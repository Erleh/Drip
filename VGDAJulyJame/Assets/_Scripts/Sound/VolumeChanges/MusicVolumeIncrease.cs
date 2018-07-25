using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeIncrease : MonoBehaviour
{

    public void onClick()
    {
        AkSoundEngine.SetRTPCValue("MusicVolume", +1f, null, 0.1); 

    }
}