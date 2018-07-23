using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuHandler: MonoBehaviour {

    public void PlayGame(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
