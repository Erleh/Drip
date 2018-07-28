using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFade : MonoBehaviour {

    [SerializeField]
    private Image deathScreen;
    [SerializeField]
    private Button[] deathScreenUIButtons;
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private float delayDuration;

    public void FadeToDeathScreen()
    {
        StartCoroutine(Delay(delayDuration));
        foreach (Button b in deathScreenUIButtons)
            StartCoroutine(FadeInImg(b.image, fadeDuration));
        StartCoroutine(FadeInImg(deathScreen, fadeDuration));
    }
    IEnumerator Delay(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
    IEnumerator FadeInImg(Image img, float duration)
    {

        img.canvasRenderer.SetAlpha(0.0f);
        img.CrossFadeAlpha(255f, duration, false);
        yield return null;
    }
}
