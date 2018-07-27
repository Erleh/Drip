using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour {


    [SerializeField]
    private List<Sprite> sprites;
    private SpriteRenderer sr;

    public void Awake()
    {
        Init();
    }
    private void Init()
    {
        if(sprites.Count > 0)
            sr = GetComponentInChildren<SpriteRenderer>();
    }
    //Grabs random sprite from entire list when it is turned on
    public void OnEnable()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
