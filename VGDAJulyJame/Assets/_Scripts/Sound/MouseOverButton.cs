using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverButton : MonoBehaviour {

	public void onMouseOver()
    {
        AkSoundEngine.PostEvent("MouseOver", gameObject);
    }

}
