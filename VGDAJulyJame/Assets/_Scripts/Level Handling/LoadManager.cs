using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LightweightPipeline;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class LoadManager
{
    public static Coroutine startLevel, endLevel;
    private static float tolerance = 1f, maxAlpha = 255f, minAlpha = 0f;
    public static IEnumerator AsyncLoadCo(string level, Image loadingImage, float duration)
    {
        Time.timeScale = 0;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
        asyncLoad.allowSceneActivation = false;
        
        loadingImage.canvasRenderer.SetAlpha(0f);
        while (!asyncLoad.isDone && loadingImage.canvasRenderer.GetAlpha() < maxAlpha - tolerance){
            loadingImage.CrossFadeAlpha(255f, duration, true);
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        endLevel = null;
        Time.timeScale = 1;
    }

    public static IEnumerator BeginLevelCo(Image loadingImage, float duration, float delay)
    {
        Time.timeScale = 0;
        loadingImage.canvasRenderer.SetAlpha(255f);
        while (loadingImage.canvasRenderer.GetAlpha() > minAlpha + tolerance)
        {
            loadingImage.CrossFadeAlpha(0, duration, true);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(delay);
        startLevel = null;
        Time.timeScale = 1;
    }
}
