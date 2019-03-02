using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class LoadManager
{
    
    static IEnumerator AsyncLoadCo(string level, Image loadingImage, float duration)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
        while (!asyncLoad.isDone)
        {
            loadingImage.canvasRenderer.SetAlpha(0.0f);
            loadingImage.CrossFadeAlpha(255f, duration, false);
            yield return null;
        }
    }

    /*static IEnumerator BeginLevelCo()
    {
        
    }*/
}
