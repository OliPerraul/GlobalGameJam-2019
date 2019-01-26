using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public CanvasGroup textBubble;
    public float fadeTime = 1.5f;

    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Start()
    {
        textBubble.alpha = 0;
    }
    
    /// <summary>
    /// Fades in.
    /// </summary>
    /// <param name="obj">Object.</param>
    private void FadeIn(CanvasGroup obj)
    {
        StartCoroutine(Fade(obj, 0, 1, fadeTime));
    }
    
    /// <summary>
    /// Fades the out.
    /// </summary>
    /// <param name="obj">Object.</param>
    private void FadeOut(CanvasGroup obj)
    {
        StartCoroutine(Fade(obj, 1, 0, fadeTime));
    }
    
    /// <summary>
    /// Fade the specified target, start, end and time.
    /// </summary>
    /// <returns>The fade.</returns>
    /// <param name="target">Target.</param>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    /// <param name="time">Time.</param>
    private IEnumerator Fade(CanvasGroup target, float start, float end, float time)
    {
        float startFade = start;
        float endFade = end;
        float currentTime = 0;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            target.alpha = Mathf.Lerp(startFade, endFade, currentTime / time);
            yield return null;
        }

        target.alpha = endFade;
    }

    #region DEV fake
    [Header("Dev Fake")]
    public bool fadeIn = false;
    public bool fadeOut = false;
    
    /// <summary>
    /// On the validate.
    /// </summary>
    private void OnValidate()
    {
        if (fadeIn)
        {
            fadeIn = false;
            FadeIn(textBubble);
        }
        
        if (fadeOut)
        {
            fadeOut = false;
            FadeOut(textBubble);
        }
    }
    #endregion
    
}
