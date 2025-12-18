using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PantallaCarga : MonoBehaviour
{
    [SerializeField] private AnimationCurve curva;
    [SerializeField] private float duracion = 1f;
    [SerializeField] private Image fadeImage;
    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(AnimarFade(fadeImage,curva, duracion, 1f));
    }
    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(AnimarFade(fadeImage,curva, duracion, 0f));
    }

    private IEnumerator AnimarFade(Image image, AnimationCurve curve, float duration, float targetAlpha)
    {
        float startAlpha = image.color.a;
        float time = 0f;

        while (time < duration)
        {
            float t = Mathf.Clamp01(time / duration);
            float curveValue = curve.Evaluate(t);

            float alpha = Mathf.LerpUnclamped(startAlpha, targetAlpha, curveValue);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        // Asegurarse de que el alpha final sea exactamente el objetivo
        image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
    }

    public void PantallaNegro()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
    }
}