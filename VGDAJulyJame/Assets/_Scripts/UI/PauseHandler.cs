using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour {

    [SerializeField]
    private GameObject PauseMenu;

    private bool paused;

	void Update ()
    {
        if (Input.GetButtonDown("Pause"))
            HandlePause();
	}
    public void HandlePause()
    {
        Debug.Log("Handling Pause...");
        if (!paused)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        paused = !paused;
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
}
