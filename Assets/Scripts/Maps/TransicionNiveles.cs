using UnityEngine;
using System.Collections;

public class TransicionNiveles : MonoBehaviour
{
    public ScriptableObject nivelDestino;
    public PantallaCarga PantallaCarga;
    public float duracion;


    public void CambiarNivel()
    {
        StartCoroutine(Transicion());
    }

    private IEnumerator Transicion()
    {
        yield return StartCoroutine(PantallaCarga.FadeIn(duracion));
        GameInitiator.instance.gameData = nivelDestino;
        yield return StartCoroutine(GameInitiator.instance.Start()); 
    }
}

