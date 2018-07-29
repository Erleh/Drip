using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WinGame : MonoBehaviour {

    [SerializeField]
    private UnityEvent winGame;
	
	// Update is called once per frame
    void OnTriggerEnter2D(Collider2D col) {
        winGame.Invoke();
	}
}
