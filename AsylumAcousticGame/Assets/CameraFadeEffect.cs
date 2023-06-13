using System;
using System.Collections;
using UnityEngine;

public class CameraFadeEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeInTime = 2f;
    public float delayTime = 2f;
    public float fadeOutTime = 2f;
    public event Action onComplete;

    private void OnEnable()
    {
        StartCoroutine(FadeEffectCoroutine());
    }

    IEnumerator FadeEffectCoroutine()
    {
        // Fade In
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeInTime;
            yield return null;
        }

        canvasGroup.alpha = 0;

        // Delay
        yield return new WaitForSeconds(delayTime);

        // Fade Out
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeOutTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
        onComplete?.Invoke();
    }
}