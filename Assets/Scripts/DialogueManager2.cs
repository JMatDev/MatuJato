using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueManager2 : MonoBehaviour
{
    public InputActionReference move;

    public void StartDialogue(int dialogueId,float zoomCamara, float camPosX, float camPosY)
    {
        StartCoroutine(DialogueCoroutine(dialogueId, zoomCamara, camPosX, camPosY));
    }

    private IEnumerator DialogueCoroutine(int dialogueId, float zoomCamara, float camPosX, float camPosY)
    {
        float startZoom = Camera.main.orthographicSize;
        Vector3 startPos = Camera.main.transform.position;

        yield return PausarJuego(zoomCamara, camPosX, camPosY);
        // mostrar la caja de diálogo
        // cargar y mostrar las líneas de diálogo correspondientes al dialogueId
        // esperar a que el jugador avance el diálogo
        // ocultar la caja de diálogo
        yield return new WaitForSecondsRealtime(2f);
        yield return ReanudarJuego(startZoom, startPos);
        yield return null;
    }

    private IEnumerator PausarJuego(float zoomCamara, float camPosX, float camPosY)
    {   
        move.action.Disable();
        Time.timeScale = 0f; // Pausa el juego
        yield return StartCoroutine(ZoomCamara(zoomCamara, camPosX, camPosY));
        yield return null;
    }

    private IEnumerator ZoomCamara(float zoomCamara, float camPosX, float camPosY)
    {
        float prevZoom = Camera.main.orthographicSize;
        Vector3 prevPos = Camera.main.transform.position;

        float duration = 1f; // Duración del movimiento
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            // Interpolación suave (ease-in-out)
            t = t * t * (3f - 2f * t);

            Camera.main.orthographicSize = Mathf.Lerp(prevZoom, zoomCamara, t);
            Camera.main.transform.position = Vector3.Lerp(prevPos, new Vector3(camPosX, camPosY, prevPos.z), t);
            yield return null;
        }
    }

    private IEnumerator ReanudarJuego(float startZoom, Vector3 startPos)
    {
        yield return StartCoroutine(ZoomCamara(startZoom, startPos.x, startPos.y));
        Time.timeScale = 1f; // Reanuda el juego
        move.action.Enable();
        yield return null;
    }
}
