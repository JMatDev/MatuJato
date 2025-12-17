using UnityEngine;

public class TransicionNiveles : MonoBehaviour
{
    public ScriptableObject nivelDestino;   
    public void CambiarNivel()
    {
        /*
        //call game initiator fade in and fade out methods
        GameInitiator.instance.StartCoroutine(GameInitiator.instance.FadeIn());
        Debug.Log("Cambio de Nivel");
        GameInitiator.instance.StartCoroutine(GameInitiator.instance.FadeOut());
        */

        GameInitiator.instance.gameData = nivelDestino;
        StartCoroutine(GameInitiator.instance.Start()); 
    }
}
