using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustmentButton : MonoBehaviour {

    public void onRelease()
    {
        AkSoundEngine.PostEvent("Release_Adjustment", gameObject);
    }

    public void onMouseOver()
    {
        AkSoundEngine.PostEvent("MouseOver", gameObject);
    }
    public void onClick()
    {
        AkSoundEngine.PostEvent("MouseOver", gameObject);
    }
    public void PointerDown()
    {
        AkSoundEngine.PostEvent("Hold_Adjuster", gameObject);
    }
    public void PointerUp()
    {
        AkSoundEngine.PostEvent("Release_Hold", gameObject);
    }
}
