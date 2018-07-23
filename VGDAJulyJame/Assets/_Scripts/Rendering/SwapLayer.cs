using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLayer : MonoBehaviour {


    [SerializeField]
    private TriggerType objTrigger;

    enum TriggerType { Upper, Lower };

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<SpriteRenderer>())
        {
            if (objTrigger == TriggerType.Upper)
            {
                print("hit");
                col.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "first";
            }
            else
            {
                col.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "third";
            }
        }
    }
}
