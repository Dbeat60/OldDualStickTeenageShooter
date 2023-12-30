using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenFader : MonoBehaviour
{

    public bool fading { private set; get; }

    private Image fadeImage;

    [SerializeField]
    private float fadeInTime;
    [SerializeField]
    private float fadeOutTime;
    [SerializeField]
    private float interactiveThresshold = 0.1f;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        fading = false;
    }

    private void Update()
    {
        fadeImage.raycastTarget = fadeImage.color.a <= interactiveThresshold;
    }

    public void FadeIn()
    {
        if (fading)
        {
            Debug.LogWarning("ScreenFade " + name + " is busy");
            return;
        }

        gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        if (fading)
        {
            Debug.LogWarning("ScreenFade " + name + " is busy");
            return;
        }
        if (!gameObject.activeSelf)
        {
            return;
        }

        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        fading = true;

        Color color = fadeImage.color;
        float alpha = color.a;
        float time = alpha * fadeInTime;
        while (time < fadeInTime)
        {
            yield return null;

            time = Mathf.Min(time + Time.deltaTime, fadeInTime);
            alpha = time / fadeInTime;
            color.a = alpha;
            fadeImage.color = color;
        }

        color.a = 1.0f;
        fadeImage.color = color;

        fading = false;
    }

    private IEnumerator FadeOutCoroutine()
    {
        fading = true;

        Color color = fadeImage.color;
        float alpha = color.a;
        float time = alpha * fadeOutTime;
        while (time > 0.0f)
        {
            yield return null;

            time = Mathf.Max(time - Time.deltaTime, 0.0f);
            alpha = time / fadeInTime;
            color.a = alpha;
            fadeImage.color = color;
        }

        color.a = 0.0f;
        fadeImage.color = color;

        gameObject.SetActive(false);
        fading = false;
    }

}
