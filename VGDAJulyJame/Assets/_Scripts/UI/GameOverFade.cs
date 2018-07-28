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

    public void FadeToDeathScreen()
    {
        foreach (Button b in deathScreenUIButtons)
            StartCoroutine(FadeInImg(b.image, fadeDuration));
        StartCoroutine(FadeInImg(deathScreen, fadeDuration));
    }
    IEnumerator FadeInImg(Image img, float duration)
    {
        img.CrossFadeAlpha(255f, duration, true);
        yield return null;
    }
}
