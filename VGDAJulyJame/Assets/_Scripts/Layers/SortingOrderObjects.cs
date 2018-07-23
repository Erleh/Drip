﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderObjects : MonoBehaviour
{
	void Start ()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 
            Mathf.RoundToInt(transform.position.y - transform.lossyScale.y * 100f) * -1;
    }
}
