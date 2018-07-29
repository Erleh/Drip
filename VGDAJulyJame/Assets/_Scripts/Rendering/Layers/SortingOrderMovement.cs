using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderMovement : MonoBehaviour
{
	void Update ()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 
            Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
