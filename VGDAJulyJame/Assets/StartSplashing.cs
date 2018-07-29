using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSplashing : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AkSoundEngine.PostEvent("Splashing", gameObject);
        AkSoundEngine.PostEvent("Monster_Drip", gameObject);
	}
}
