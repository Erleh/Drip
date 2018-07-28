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
        foreach (Button b in deathScreenUIButtons)
            StartCoroutine(FadeInAfterDelay(b.image, fadeDuration, delayDuration));
        StartCoroutine(FadeInAfterDelay(deathScreen, fadeDuration, delayDuration));
    }
    IEnumerator FadeInAfterDelay(Image img, float duration, float delay)
    {
        img.canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForSeconds(delay);
        img.CrossFadeAlpha(255f, duration, false);
    }
}
