using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    public Image fadeOutImage, fadeInImage;
    public float fadeToGameSeconds, fadeToLoadSeconds, delayBeforeInput;
    public string levelToLoad;
    void Start()
    {
        DontDestroyOnLoad(this);
        LoadManager.startLevel = StartCoroutine(LoadManager.BeginLevelCo(fadeOutImage, fadeToGameSeconds, delayBeforeInput));
    }

    public void LoadNextLevel()
    {
        LoadManager.endLevel = StartCoroutine(LoadManager.AsyncLoadCo(levelToLoad, fadeInImage, fadeToLoadSeconds));
    }
}
