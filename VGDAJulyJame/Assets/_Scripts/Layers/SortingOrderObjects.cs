using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderObjects : MonoBehaviour
{
	void Awake()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 
            Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
