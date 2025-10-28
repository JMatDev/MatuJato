using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public AnimationCurve curve;
    public InputActionReference move;
    public GameObject prefab_dialogueBox;
    public Canvas canvas;
    public LectorGuion lectorGuion;
    public TextAsset csvFile;


    private GameObject Instance_dialogueBox;
    private float startZoom;
    private Vector3 startPos;

    public void StartDialogue(TextAsset csvFile, float zoomCamara, float camPosX, float camPosY)
    {
        StartCoroutine(DialogueCoroutine(csvFile, zoomCamara, camPosX, camPosY));
    }

    private IEnumerator DialogueCoroutine(TextAsset csvFile, float zoomCamara, float camPosX, float camPosY)
    {
        startZoom = Camera.main.orthographicSize;
        startPos = Camera.main.transform.position;

        yield return PausarYzoom(zoomCamara, camPosX, camPosY);
        yield return MostrarCajaDialogo();

        yield return lectorGuion.LeerGuion(csvFile, Instance_dialogueBox);
        
        yield return QuitarCajaDialogo(Instance_dialogueBox);  
        yield return ReanudarYzoom(startZoom, startPos);
        yield return null;
    }

    private IEnumerator PausarYzoom(float zoomCamara, float camPosX, float camPosY)
    {   
        move.action.Disable();
        Time.timeScale = 0f; // Pausa el juego
        yield return ZoomCamara(zoomCamara, camPosX, camPosY);
        yield return null;
    }

    private IEnumerator ZoomCamara(float zoomCamara, float camPosX, float camPosY, float duration = 1f)
    {
        float prevZoom = Camera.main.orthographicSize;
        Vector3 prevPos = Camera.main.transform.position;

        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duration)
        {
            float t = Mathf.Clamp01(tiempoTranscurrido / duration);
            float curveValue = curve.Evaluate(t);

            Camera.main.orthographicSize = Mathf.LerpUnclamped(prevZoom, zoomCamara, curveValue);
            Camera.main.transform.position = Vector3.LerpUnclamped(prevPos, new Vector3(camPosX, camPosY, prevPos.z), curveValue);

            tiempoTranscurrido += Time.unscaledDeltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = zoomCamara;
        Camera.main.transform.position = new Vector3(camPosX, camPosY, prevPos.z);   
    }

    private IEnumerator ReanudarYzoom(float startZoom, Vector3 startPos)
    {
        Time.timeScale = 1f; // Reanuda el juego
        move.action.Enable();
        yield return StartCoroutine(ZoomCamara(startZoom, startPos.x, startPos.y));
        yield return null;
    }

    private IEnumerator MostrarCajaDialogo()
    {
        Instance_dialogueBox = Instantiate(prefab_dialogueBox, canvas.transform);
        yield return null;
    }

    private IEnumerator QuitarCajaDialogo(GameObject dialogueBox)
    {
        Destroy(dialogueBox);
        yield return null;
    }
}
