using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour {

	public void onClick()
    {
        AkSoundEngine.PostEvent("Click_Setting", gameObject);
    }

}
