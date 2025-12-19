using UnityEngine;
using System.Collections;

public class TransicionNiveles : MonoBehaviour
{
    public ScriptableObject nivelDestino;
    public PantallaCarga PantallaCarga;
    public float duracion;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoPuerta;


    public void CambiarNivel()
    {
        StartCoroutine(Transicion());
    }

    private IEnumerator Transicion()
    {
        // 🔊 Reproducir sonido de puerta
        if (audioSource != null && sonidoPuerta != null)
            audioSource.PlayOneShot(sonidoPuerta);
        // ⏳ Pequeña espera para que el sonido se escuche
        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(PantallaCarga.FadeIn(duracion));
        GameInitiator.instance.gameData = nivelDestino;
        yield return StartCoroutine(GameInitiator.instance.Start());
    }
}

