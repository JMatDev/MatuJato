using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainMenuInitiator : MonoBehaviour
{
    public Canvas canvas;
    public Image LogoInicial;
    public AnimationCurve curvaAparicion;
    public float duracionAparicion;
    public float duracionEspera;
    public float duracionDesaparicion;
    public Animator boxAnimator;
    public InputActionAsset inputActionAsset;

    void Start()
    {
        StartCoroutine(inicioDeMenu());
    }

    private IEnumerator inicioDeMenu()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(mostrarImagen(LogoInicial, curvaAparicion, duracionAparicion, true));
        yield return new WaitForSeconds(duracionEspera);
        yield return StartCoroutine(mostrarImagen(LogoInicial, curvaAparicion, duracionDesaparicion, false));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(activarActionMaps());
        boxAnimator.SetTrigger("Aparecer");
    }

    private IEnumerator mostrarImagen(Image image, AnimationCurve curve, float duration, bool isAppearing)
    {
        if (isAppearing) image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        else image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        float targetAlpha = isAppearing ? 1f : 0f;
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

    private IEnumerator activarActionMaps()
    {
        var gameplayMap = inputActionAsset.FindActionMap("MainMenu");
        
        foreach (var action in gameplayMap.actions)
        {
            action.Enable();  
        }

        yield return null;
    }
}
