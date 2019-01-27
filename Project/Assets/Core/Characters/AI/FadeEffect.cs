using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image[] emotions;
    public CanvasGroup textBubble;
    public float fadeTime = 1.5f;
    public float autoFadeInterval = 2f;

    private float _currentTime = 0;
    private float _randomFadeTime = 0;
    private bool _isFadedIn = true;

    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Start()
    {
        textBubble.alpha = 0;
        _randomFadeTime = Random.value * autoFadeInterval + fadeTime;
        foreach (Image image in emotions)
        {
            image.enabled = false;
        }
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _randomFadeTime)
        {
            _currentTime = 0;
            _randomFadeTime = Random.value * autoFadeInterval + fadeTime;
            FadeTrigger();
        }
    }

    #region Fade Management

    private void FadeTrigger()
    {
        if (_isFadedIn)
        {
            FadeIn(textBubble);
            emotions[Random.Range(0, emotions.Length-1)].enabled = true;
            _isFadedIn = false;
        }
        else
        {
            FadeOut(textBubble);
            _isFadedIn = true;
        }
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
    #endregion


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
