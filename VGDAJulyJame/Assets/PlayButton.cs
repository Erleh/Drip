using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

	public void onClick()
    {
        AkSoundEngine.PostEvent("Click_Play", gameObject);
    }

}
