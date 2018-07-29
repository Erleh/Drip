using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {
    
    public void onClick()
    {
        AkSoundEngine.SetState("PlayerLife", "Alive");
        AkSoundEngine.SetState("Moving", "Idle");

    }
}
