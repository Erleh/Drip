using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader : MonoBehaviour {

    /// <summary>
    /// Fades sprites in on enable, waits for a set duration, then fades out. Alpha is set back to 0 on disable
    /// </summary>
    private SpriteRenderer sr;
    private Color origColor;

    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private float fadeToAlpha;
    [SerializeField]
    private float betweenTransition;
    private Coroutine fading;
    private bool fadeOut;
    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        origColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
    }
    //update determines when object fades out
    private void Update()
    {
        if(fading == null)
            BeginFadeOut();
    }
    //fades to determined fadeToAlpha over given duration using FadeTo coroutine
    private void BeginFadeIn()
    {
        fading = StartCoroutine(FadeTo(fadeToAlpha, fadeDuration));
    }
    //uses the same coroutine, but backwards
    private void BeginFadeOut()
    {
        fadeOut = true;
        fading = StartCoroutine(FadeTo(0, fadeDuration));
    }
    //Fades to value over given duration
    IEnumerator FadeTo(float value, float duration)
    {
        float alpha = sr.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.MoveTowards(alpha, value, t));
            sr.color = newColor;
            yield return null;
        }
        //if the object is fading in
        if(!fadeOut)
            yield return new WaitForSeconds(betweenTransition);
        fading = null;
    }
    //on enable, begin coroutine
    private void OnEnable()
    {
        BeginFadeIn();
    }
    //Reset values to original 
    private void OnDisable()
    {
        fading = null;
        fadeOut = false;
        sr.color = origColor;
    }
}
