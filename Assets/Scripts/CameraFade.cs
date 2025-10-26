using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    [Header("Fade Settings")]
    public Color fadeColor = Color.black;
    

    [Header("UI Reference")]
    [SerializeField] private Image fadeImage;
    
    private void Awake()
    {
        if (fadeImage == null)
        {
            DebugLogger.Log(LogChannel.UI, "CameraFadeUI: No Image assigned! Please assign a UI Image in the inspector.", LogLevel.Error);
            enabled = false;
            return;
        }

        // Initialize the fade color (usually fully transparent)
        Color startColor = fadeColor;
        startColor.a = 0f;
        fadeImage.color = startColor;
    }

    public void TurnOnFade()
    {
        fadeImage.gameObject.SetActive(true);
        enabled = true;
    }

    public void TurnOffFade()
    {
        fadeImage.gameObject.SetActive(false);
        StopFadeLoop();
        enabled = false;
    }
    
    public void StartFadeLoop(float waitTime = 2f)
    {
        StartCoroutine(FadeLoop(1.5f, waitTime)); 
    }

    public void StopFadeLoop()
    {
        StopAllCoroutines();
    }
    
    private IEnumerator FadeOut(float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / duration);
            fadeImage.color = color;
            yield return null;
        }

        SetAlpha(1f);
    }

    private IEnumerator FadeIn(float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            fadeImage.color = color;
            yield return null;
        }

        SetAlpha(0f);
    }
    
    private IEnumerator FadeLoop(float fadeDuration, float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(FadeOut(fadeDuration));
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(FadeIn(fadeDuration));
            
        }
    }
    
    private void SetAlpha(float alpha)
    {
        Color c = fadeImage.color;
        c.a = alpha;
        fadeImage.color = c;
    }
}
