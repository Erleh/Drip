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
        StartCoroutine(Delay(fadeDuration));
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
        img.CrossFadeAlpha(1, duration, true);
        yield return null;
    }
}
