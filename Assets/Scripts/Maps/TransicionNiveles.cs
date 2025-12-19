using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class TransicionNiveles : MonoBehaviour
{
    public ScriptableObject nivelDestino;
    public PantallaCarga PantallaCarga;
    public float duracion;
    public characterStats characterStats;


    public void CambiarNivel()
    {
        if(characterStats.tieneLlave == false)
        {
           SoundFXManager.instance.PlaySound(SoundType.PUERTA_BLOQQUEADA,0.7f);
        }
        else
        {
            StartCoroutine(Transicion());
        }    
    }

    private IEnumerator Transicion()
    {
        SoundFXManager.instance.PlaySound(SoundType.ABRIR_PUERTA,0.7f);
        yield return StartCoroutine(PantallaCarga.FadeIn(duracion));
        GameInitiator.instance.gameData = nivelDestino;
        yield return StartCoroutine(GameInitiator.instance.Start()); 
    }
}

