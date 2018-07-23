using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface BreakableBase
{
    // if object is destroyed
    void OnDestroyed();
    // if object is interacted with
    void OnInteraction(bool aggressive, Collision2D col);
}
