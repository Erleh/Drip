using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {
    
    public void ResetSounds()
    {
        AkSoundEngine.SetState("PlayerLife", "Alive");
        AkSoundEngine.SetState("Moving", "Idle");

    }
}
